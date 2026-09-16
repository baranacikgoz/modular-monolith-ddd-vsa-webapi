using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace Common.Tests.Architecture;

#pragma warning disable CA1515 // Consider making public types internal

/// <summary>
/// StronglyTypedIdValueConverter maps a strongly-typed id to its raw column via id => id.Value.
/// EF Core translates a whole-value query comparison (m.Id == typedId) to SQL fine, but a
/// member-access-then-compare on that same converted property (m.Id.Value == rawGuid) throws
/// "could not be translated" only when the query actually runs. Both forms compile with zero
/// warnings, so this test turns the mistake into a build-time failure instead of a runtime one
/// (CLAUDE.md &#167;4). See STRONGLY_TYPED_ID_VALUE_COMPARISON_FOOTGUN.md for the incident history.
/// </summary>
public sealed class StronglyTypedIdQueryTests
{
    /// <summary>
    /// Proves the detector's algorithm on a small in-memory fixture before trusting it against the
    /// whole repository: it must flag a strongly-typed id's raw .Value read inside an EF query
    /// lambda when rooted at the query's own parameter, and it must leave alone the two shapes that
    /// look similar but are not the bug (a whole-id compare, and a .Value read rooted in a captured
    /// closure variable, which EF evaluates client-side into a SQL parameter).
    /// </summary>
    [Fact]
    public void Detector_FlagsQueryRootedAccess_AndIgnoresClosureRootedAccess()
    {
        const string fixtureSource = """
            using System;
            using System.Linq;
            using System.Linq.Expressions;
            using Common.Domain.StronglyTypedIds;

            namespace Fixture;

            public readonly record struct FixtureId(Guid Value) : IStronglyTypedId;

            public class FixtureEntity
            {
                public FixtureId Id { get; set; }
                public Guid RawId { get; set; }
            }

            public static class FixtureExtensions
            {
                public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, bool condition)
                    => condition ? query.Where(predicate) : query;
            }

            public static class FixtureQueries
            {
                public static IQueryable<FixtureEntity> BuggyWhere(IQueryable<FixtureEntity> entities, Guid raw)
                    => entities.Where(e => e.Id.Value == raw); // VIOLATION

                public static IQueryable<FixtureEntity> BuggyWhereIf(IQueryable<FixtureEntity> entities, Guid raw, bool condition)
                    => entities.WhereIf(e => e.Id.Value == raw, condition); // VIOLATION

                public static IQueryable<FixtureEntity> ValidWholeIdCompare(IQueryable<FixtureEntity> entities, FixtureId id)
                    => entities.Where(e => e.Id == id);

                public static IQueryable<FixtureEntity> ValidClosureRootedValue(IQueryable<FixtureEntity> entities, FixtureId captured)
                    => entities.Where(e => e.RawId == captured.Value);
            }
            """;

        var expectedLines = fixtureSource
            .Split('\n')
            .Select((line, index) => (line, number: index + 1))
            .Where(x => x.line.Contains("// VIOLATION", StringComparison.Ordinal))
            .Select(x => x.number)
            .Order()
            .ToArray();

        var compilation = BuildCompilation([("Fixture.cs", fixtureSource)]);

        var violations = StronglyTypedIdQueryDetector.FindViolations(compilation);

        Assert.Equal(expectedLines, violations.Select(v => v.Line).Order().ToArray());
    }

    /// <summary>
    /// The real scan: no query in the repository may reach past a strongly-typed id's converted
    /// column and read the raw .Value underneath it.
    /// </summary>
    [Fact]
    public void Queries_MustNotAccessStronglyTypedIdValue()
    {
        var srcRoot = Path.Combine(TestPaths.RepositoryRoot, "src");
        var sourceFiles = Directory
            .EnumerateFiles(srcRoot, "*.cs", SearchOption.AllDirectories)
            .Where(IsScannable)
            .Select(path => (path, File.ReadAllText(path)))
            .ToList();

        var compilation = BuildCompilation(sourceFiles);

        var stronglyTypedId = compilation.GetTypeByMetadataName("Common.Domain.StronglyTypedIds.IStronglyTypedId");
        var expressionOfT = compilation.GetTypeByMetadataName("System.Linq.Expressions.Expression`1");
        Assert.True(stronglyTypedId is not null, "IStronglyTypedId did not resolve in the scan compilation: reference discovery is broken.");
        Assert.True(expressionOfT is not null, "Expression<T> did not resolve in the scan compilation: reference discovery is broken.");

        var expressionLambdaCount = CountExpressionLambdas(compilation, expressionOfT);
        Assert.True(
            expressionLambdaCount >= 25,
            $"Only found {expressionLambdaCount} Expression<> query lambdas across src/**/*.cs; expected at least "
            + "25. The scan's source/reference discovery likely regressed and is silently under-reporting instead "
            + "of actually finding a clean repository.");

        var violations = StronglyTypedIdQueryDetector.FindViolations(compilation);

        Assert.True(
            violations.Count == 0,
            "A strongly-typed id's raw .Value was read inside an EF query lambda, rooted at the query's own "
            + "parameter (CLAUDE.md §4). EF translates a whole-id compare (m.Id == typedId) fine but throws "
            + "on a member-access-then-compare on the converted property (m.Id.Value == rawGuid). Wrap the raw "
            + "value first instead: new TId(rawGuid). See StronglyTypedIdValueConverter's remarks.\n"
            + string.Join('\n', violations));
    }

    private static bool IsScannable(string path)
    {
        var normalized = path.Replace(Path.DirectorySeparatorChar, '/');
        return !normalized.Contains("/obj/", StringComparison.Ordinal)
               && !normalized.Contains("/bin/", StringComparison.Ordinal)
               && !normalized.Contains("/Migrations/", StringComparison.Ordinal);
    }

    private static int CountExpressionLambdas(CSharpCompilation compilation, INamedTypeSymbol expressionOfT)
    {
        var count = 0;

        foreach (var tree in compilation.SyntaxTrees)
        {
            var model = compilation.GetSemanticModel(tree);
            foreach (var lambda in tree.GetRoot().DescendantNodes().OfType<LambdaExpressionSyntax>())
            {
                if (ConvertsTo(lambda, model, expressionOfT))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static bool ConvertsTo(ExpressionSyntax expression, SemanticModel model, INamedTypeSymbol target)
    {
        return model.GetTypeInfo(expression).ConvertedType is INamedTypeSymbol named
               && SymbolEqualityComparer.Default.Equals(named.OriginalDefinition, target);
    }

    /// <summary>
    /// Builds a standalone compilation from raw source text (no MSBuildWorkspace, no project system):
    /// every assembly in the CoreCLR and ASP.NET shared frameworks, plus every module assembly this
    /// test run's own output directory already carries (Products.Endpoints, Inventory.Domain, EF Core,
    /// ...), is fed back in as a metadata reference. The SDK's generated implicit-usings file is
    /// recreated by hand since we never ran a real build of these sources.
    /// </summary>
    private static CSharpCompilation BuildCompilation(IEnumerable<(string Path, string Text)> sourceFiles)
    {
        var parseOptions = new CSharpParseOptions(LanguageVersion.Preview);

        var trees = sourceFiles
            .Select(f => CSharpSyntaxTree.ParseText(f.Text, parseOptions, path: f.Path))
            .Append(CSharpSyntaxTree.ParseText(ImplicitUsingsSource, parseOptions, path: "ImplicitUsings.g.cs"))
            .ToList();

        return CSharpCompilation.Create(
            "StronglyTypedIdQueryScan",
            trees,
            References(),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private static List<MetadataReference> References()
    {
        // Listed in preference order: when the same file name exists in more than one directory
        // (e.g. a BCL assembly present both in the shared framework and copied into this test
        // run's own output), the first hit wins, so every project consistently resolves core types
        // like IQueryable<T> against the same canonical shared-framework copy.
        string[] directories =
        [
            Path.GetDirectoryName(typeof(object).Assembly.Location)!,
            Path.GetDirectoryName(typeof(Microsoft.AspNetCore.Http.HttpContext).Assembly.Location)!,
            AppContext.BaseDirectory,
        ];

        return directories
            .SelectMany(dir => Directory.EnumerateFiles(dir, "*.dll"))
            .GroupBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .Select(TryCreateReference)
            .OfType<MetadataReference>()
            .ToList();
    }

    private static MetadataReference? TryCreateReference(string path)
    {
        try
        {
            return MetadataReference.CreateFromFile(path);
        }
        catch (Exception ex) when (ex is BadImageFormatException or IOException)
        {
            return null; // a native/mixed-mode dll sitting alongside managed ones in the same directory
        }
    }

    private const string ImplicitUsingsSource = """
        global using System;
        global using System.Collections.Generic;
        global using System.IO;
        global using System.Linq;
        global using System.Net.Http;
        global using System.Net.Http.Json;
        global using System.Threading;
        global using System.Threading.Tasks;
        global using Microsoft.AspNetCore.Builder;
        global using Microsoft.AspNetCore.Hosting;
        global using Microsoft.AspNetCore.Http;
        global using Microsoft.AspNetCore.Routing;
        global using Microsoft.Extensions.Configuration;
        global using Microsoft.Extensions.DependencyInjection;
        global using Microsoft.Extensions.Hosting;
        global using Microsoft.Extensions.Logging;
        global using DefaultIdType = System.Guid;
        """;
}

/// <summary>
/// A single flagged read: a strongly-typed id's converted .Value column, reached from inside an
/// EF query lambda and rooted at that lambda's own parameter rather than a captured closure variable.
/// </summary>
internal readonly record struct StronglyTypedIdQueryViolation(string FilePath, int Line, int Column, string Snippet)
{
    public override string ToString() => $"{FilePath}({Line},{Column}): {Snippet}";
}

/// <summary>
/// Flags a strongly-typed id's raw .Value read inside an EF query lambda, when it is rooted at that
/// lambda's own parameter. Three conditions must all hold, in this order (cheapest first):
/// (1) the ".Value" being read belongs to a type implementing IStronglyTypedId, not Nullable&lt;T&gt;,
/// Result&lt;T&gt;, IOptions&lt;T&gt;, Claim, or any other unrelated ".Value";
/// (2) the read sits inside a lambda handed directly to an IQueryable&lt;T&gt;-shaped method (.Where,
/// .Select, the repo's own .WhereIf, ...), i.e. a lambda EF actually translates to SQL. This is
/// deliberately narrower than "any Expression&lt;TDelegate&gt;-converted lambda": that broader net also
/// catches StronglyTypedIdValueConverter's own id =&gt; id.Value, which is registered through
/// ValueConverter's Expression&lt;Func&lt;,&gt;&gt;-typed constructor and is the correct, canonical unwrap the
/// whole mechanism depends on, not a query mistake;
/// (3) the receiver chain roots at a parameter of that query lambda, not a captured outer variable:
/// closure-rooted reads like `.Where(e => e.RawId == captured.Value)` are evaluated client-side into
/// a SQL parameter and translate fine.
/// </summary>
internal static class StronglyTypedIdQueryDetector
{
    public static List<StronglyTypedIdQueryViolation> FindViolations(Compilation compilation)
    {
        var stronglyTypedId = compilation.GetTypeByMetadataName("Common.Domain.StronglyTypedIds.IStronglyTypedId");
        var queryableOfT = compilation.GetTypeByMetadataName("System.Linq.IQueryable`1");
        var violations = new List<StronglyTypedIdQueryViolation>();

        if (stronglyTypedId is null || queryableOfT is null)
        {
            return violations;
        }

        foreach (var tree in compilation.SyntaxTrees)
        {
            var model = compilation.GetSemanticModel(tree);

            foreach (var memberAccess in tree.GetRoot().DescendantNodes().OfType<MemberAccessExpressionSyntax>())
            {
                if (!string.Equals(memberAccess.Name.Identifier.Text, "Value", StringComparison.Ordinal))
                {
                    continue;
                }

                if (model.GetSymbolInfo(memberAccess).Symbol is not IPropertySymbol { ContainingType: { } containingType })
                {
                    continue;
                }

                if (!Implements(containingType, stronglyTypedId))
                {
                    continue;
                }

                var enclosingLambdas = EnclosingLambdas(memberAccess).ToList();
                if (enclosingLambdas.Count == 0
                    || !enclosingLambdas.Any(l => IsQueryableLambdaArgument(l, model, queryableOfT))
                    || !IsRootedInLambdaParameter(memberAccess.Expression, enclosingLambdas, model))
                {
                    continue;
                }

                var position = memberAccess.GetLocation().GetLineSpan().StartLinePosition;
                violations.Add(new StronglyTypedIdQueryViolation(
                    tree.FilePath, position.Line + 1, position.Character + 1, memberAccess.ToString()));
            }
        }

        return violations;
    }

    private static bool Implements(INamedTypeSymbol type, INamedTypeSymbol stronglyTypedId)
    {
        return SymbolEqualityComparer.Default.Equals(type, stronglyTypedId)
               || type.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, stronglyTypedId));
    }

    /// <summary>True when <paramref name="lambda"/> is an argument of a call whose receiver (for an extension method, its "this" parameter; otherwise its declaring instance) is <c>IQueryable&lt;T&gt;</c> or something implementing it, such as EF's <c>DbSet&lt;T&gt;</c>.</summary>
    private static bool IsQueryableLambdaArgument(LambdaExpressionSyntax lambda, SemanticModel model, INamedTypeSymbol queryableOfT)
    {
        if (lambda.Parent is not ArgumentSyntax { Parent: ArgumentListSyntax { Parent: InvocationExpressionSyntax invocation } })
        {
            return false; // e.g. handed to a constructor (ValueConverter's base(...)), not a query call
        }

        if (model.GetSymbolInfo(invocation).Symbol is not IMethodSymbol method)
        {
            return false;
        }

        var receiverType = method.ReceiverType
                            ?? (method.IsExtensionMethod && method.Parameters.Length > 0 ? method.Parameters[0].Type : null);

        return receiverType is not null && IsOrImplements(receiverType, queryableOfT);
    }

    private static bool IsOrImplements(ITypeSymbol type, INamedTypeSymbol queryableOfT)
    {
        if (type is INamedTypeSymbol named && SymbolEqualityComparer.Default.Equals(named.OriginalDefinition, queryableOfT))
        {
            return true;
        }

        return type.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, queryableOfT));
    }

    /// <summary>Every lambda enclosing <paramref name="node"/>, from innermost to outermost, stopping at the containing member (method/property/field).</summary>
    private static IEnumerable<LambdaExpressionSyntax> EnclosingLambdas(SyntaxNode node)
    {
        for (var current = node.Parent; current is not null; current = current.Parent)
        {
            if (current is LambdaExpressionSyntax lambda)
            {
                yield return lambda;
            }

            if (current is MemberDeclarationSyntax)
            {
                yield break;
            }
        }
    }

    private static bool IsRootedInLambdaParameter(ExpressionSyntax receiver, IReadOnlyList<LambdaExpressionSyntax> lambdas, SemanticModel model)
    {
        var expression = receiver;
        while (expression is MemberAccessExpressionSyntax memberAccess)
        {
            expression = memberAccess.Expression;
        }

        if (expression is not IdentifierNameSyntax identifier
            || model.GetSymbolInfo(identifier).Symbol is not IParameterSymbol parameterSymbol)
        {
            return false;
        }

        return lambdas.Any(lambda => LambdaParameters(lambda).Any(
            p => SymbolEqualityComparer.Default.Equals(model.GetDeclaredSymbol(p), parameterSymbol)));
    }

    private static SeparatedSyntaxList<ParameterSyntax> LambdaParameters(LambdaExpressionSyntax lambda) => lambda switch
    {
        SimpleLambdaExpressionSyntax simple => SyntaxFactory.SingletonSeparatedList(simple.Parameter),
        ParenthesizedLambdaExpressionSyntax parenthesized => parenthesized.ParameterList.Parameters,
        _ => default,
    };
}

#pragma warning restore CA1515
