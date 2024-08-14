using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Common.SourceGenerators;

[Generator]
public class V1DeleteMyEntityEndpointSourceGenerator : IIncrementalGenerator
{
    private const string AuditableEntityInterfaceName = "IAuditableEntity";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Step 1: Get the module name
        var moduleNameProvider = context
            .CompilationProvider
            .Select((compilation, _) => GetModuleName(compilation.AssemblyName!));

        // Step 2: Get the domain project name
        var domainProjectNameProvider = moduleNameProvider
            .Select((moduleName, _) => $"{moduleName}.Domain");

        // Step 3: Find the domain assembly symbol
        var domainAssemblyProvider = context
            .CompilationProvider
            .Combine(domainProjectNameProvider)
            .Select((pair, _) =>
            {
                var (compilation, domainProjectName) = pair;
                return compilation
                        .SourceModule
                        .ReferencedAssemblySymbols
                        .SingleOrDefault(assembly => string.Equals(assembly.Name, domainProjectName, StringComparison.Ordinal));
            });

        // Step 4: Generate the endpoints for each entity implementing IAuditableEntity and having OwnerId field
        var endpointSourceProvider = domainAssemblyProvider
            .SelectMany((domainAssemblySymbol, _) => GetAuditableEntitiesHavingOwnerIdField(domainAssemblySymbol?.GlobalNamespace))
            .Combine(moduleNameProvider)
            .Select((pair, _) => GenerateDeleteEndpointCode(pair.Left.namespaceName, pair.Right, pair.Left.className));

        // Step 5: Add the source to the compilation
        context.RegisterSourceOutput(endpointSourceProvider, (context, source) =>
        {
            context.AddSource($"{source.ClassName}_DeleteEndpoint.g.cs", SourceText.From(source.SourceCode, Encoding.UTF8));
        });
    }

    private static IEnumerable<(string namespaceName, string className, string sourceCode)> GetAuditableEntitiesHavingOwnerIdField(INamespaceSymbol? rootNs)
    {
        if (rootNs is not null)
        {
            var stack = new Stack<INamespaceSymbol>();
            stack.Push(rootNs);

            while (stack.Count > 0)
            {
                foreach (var member in stack.Pop().GetMembers())
                {
                    if (member is INamespaceSymbol namespaceSymbol)
                    {
                        stack.Push(namespaceSymbol);
                    }
                    else if (member is INamedTypeSymbol namedTypeSymbol && ImplementsIAuditableEntity(namedTypeSymbol) && HasOwnerIdField(namedTypeSymbol))
                    {
                        var namespaceName = namedTypeSymbol.ContainingNamespace.ToDisplayString();
                        var className = namedTypeSymbol.Name;
                        var (SourceCode, ClassName) = GenerateDeleteEndpointCode(namespaceName, string.Empty, className);
                        yield return (namespaceName, ClassName, SourceCode);
                    }
                }
            }
        }
    }

    private static bool ImplementsIAuditableEntity(INamedTypeSymbol namedTypeSymbol)
    {
        return namedTypeSymbol
            .AllInterfaces
            .Any(i => i.Name.Contains(AuditableEntityInterfaceName));
    }

    private static bool HasOwnerIdField(INamedTypeSymbol namedTypeSymbol)
    {
        return namedTypeSymbol
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Any(p => p.Name == "OwnerId");
    }

    private static string GetModuleName(string assemblyName)
    {
        // Assuming the assembly name follows the pattern {ModuleName}.Application
        var parts = assemblyName.Split('.');
        if (parts.Length > 1 && parts[1] == "Application")
        {
            return parts[0];
        }

        throw new InvalidOperationException("Invalid assembly name");
    }

    private static (string SourceCode, string ClassName) GenerateDeleteEndpointCode(string namespaceName, string moduleName, string className)
    {
        var pluralClassName = Pluralize(className);
        var sourceCode = $@"
// WARNING: Auto-Generated by Source Generator.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.StronglyTypedIds;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using {namespaceName};
using Microsoft.Extensions.DependencyInjection;
using Common.Application.ModelBinders;
using Ardalis.Specification;

namespace {moduleName}.Application.{pluralClassName}.v1.My.Delete;

internal static class Endpoint
{{
    internal static void MapEndpoint(RouteGroupBuilder apiGroup)
    {{
        apiGroup
            .MapDelete(""{{id}}"", Delete{className}Async)
            .WithDescription(""Delete my {className}."")
            .MustHavePermission(CustomActions.DeleteMy, CustomResources.{pluralClassName})
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }}

    private sealed class {className}ByIdAndOwnerIdSpec : SingleResultSpecification<{className}>
    {{
        public {className}ByIdAndOwnerIdSpec({className}Id id, ApplicationUserId ownerId)
            => Query
                .Where(p => p.Id == id && p.OwnerId == ownerId);
    }}

    private static async Task<Result> Delete{className}Async(
        [FromRoute, ModelBinder(typeof(StronglyTypedIdBinder<{className}Id>))] {className}Id id,
        [FromServices] IRepository<{className}> repository,
        [FromServices] ICurrentUser currentUser,
        [FromKeyedServices(nameof({moduleName}))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new {className}ByIdAndOwnerIdSpec(id, currentUser.Id), cancellationToken)
            .TapAsync(entity => repository.Delete(entity))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}}
";
        return (sourceCode, className);
    }

    private static string Pluralize(string word)
    {
        if (word.EndsWith("y", StringComparison.OrdinalIgnoreCase))
        {
            return $"{word.Substring(0, word.Length - 1)}ies";
        }

        if (word.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return $"{word}es";
        }

        return $"{word}s";
    }
}
