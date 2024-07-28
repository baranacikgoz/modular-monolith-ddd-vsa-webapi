using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Common.SourceGenerators;

[Generator]
public class DeleteEndpointSourceGenerator : ISourceGenerator
{
    private const string AuditableEntityFullName = "Common.Domain.Entities.AuditableEntity";

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
        {
            context.ReportDiagnostic(Diagnostic.Create(_noSyntaxReceiverDescriptor, Location.None));
            return;
        }

        var compilation = context.Compilation;

        var auditableEntitySymbol = compilation.GetTypeByMetadataName(AuditableEntityFullName);
        if (auditableEntitySymbol == null)
        {
            context.ReportDiagnostic(Diagnostic.Create(_auditableEntityNotFoundDescriptor, Location.None));
            return;
        }

        foreach (var classDeclaration in receiver.CandidateClasses)
        {
            var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);

            if (model.GetDeclaredSymbol(classDeclaration) is not INamedTypeSymbol classSymbol)
            {
                continue;
            }

            if (!IsDerivedFrom(classSymbol, auditableEntitySymbol))
            {
                continue;
            }

            context.ReportDiagnostic(Diagnostic.Create(_classInheritsDescriptor, Location.None, classSymbol.Name, auditableEntitySymbol.Name));

            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            var className = classSymbol.Name;

            var source = GenerateDeleteEndpointCode(namespaceName, className);
            context.AddSource($"{className}_DeleteEndpoint.g.cs", SourceText.From(source, Encoding.UTF8));
            context.ReportDiagnostic(Diagnostic.Create(_endpointGeneratedDescriptor, Location.None, className));
        }
    }

    private static bool IsDerivedFrom(INamedTypeSymbol? classSymbol, INamedTypeSymbol baseTypeSymbol)
    {
        var currentType = classSymbol;
        while (currentType != null)
        {
            if (SymbolEqualityComparer.Default.Equals(currentType, baseTypeSymbol))
            {
                return true;
            }
            currentType = currentType.BaseType;
        }
        return false;
    }

    private static string GenerateDeleteEndpointCode(string namespaceName, string className)
    {
        var pluralClassName = Pluralize(className);
        return $@"
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using {namespaceName};
using Microsoft.Extensions.DependencyInjection;
using Common.Application.ModelBinders;
using Ardalis.Specification;

namespace SourceGenerated.Application.{pluralClassName}.v1.Delete;

internal static class {className}DeleteEndpoint
{{
    internal static void MapEndpoint(RouteGroupBuilder apiGroup)
    {{
        apiGroup
            .MapDelete(""{{id}}"", Delete{className}Async)
            .WithDescription(""Delete a {className}."")
            .MustHavePermission(CustomActions.Delete, CustomResources.{pluralClassName})
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }}

    private sealed class {className}ByIdSpec : SingleResultSpecification<{className}>
    {{
        public {className}ByIdSpec({className}Id id)
            => Query
                .Where(p => p.Id == id);
    }}

    private static async Task<Result> Delete{className}Async(
        [FromRoute, ModelBinder(typeof(StronglyTypedIdBinder<{className}Id>))] {className}Id id,
        [FromServices] IRepository<{className}> repository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new {className}ByIdSpec(id), cancellationToken)
            .TapAsync(entity => repository.Delete(entity))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}}
";
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

    private sealed class SyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> CandidateClasses { get; } = [];

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
            {
                CandidateClasses.Add(classDeclarationSyntax);
            }
        }
    }

    private static readonly DiagnosticDescriptor _noSyntaxReceiverDescriptor = new(
        id: "GEN001",
        title: "Syntax Receiver Not Found",
        messageFormat: "Syntax receiver not found",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _auditableEntityNotFoundDescriptor = new(
        id: "GEN002",
        title: "AuditableEntity Not Found",
        messageFormat: "AuditableEntity not found",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _endpointGeneratedDescriptor = new(
        id: "GEN003",
        title: "Endpoint Generated",
        messageFormat: "Generated delete endpoint for {0}",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _classInheritsDescriptor = new(
        id: "GEN004",
        title: "Class Inherits AuditableEntity",
        messageFormat: "{0} is derived from {1}",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);
}
