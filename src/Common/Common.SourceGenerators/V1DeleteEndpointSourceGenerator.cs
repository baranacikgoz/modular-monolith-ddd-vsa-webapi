using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Common.SourceGenerators;

[Generator]
public class V1DeleteEndpointSourceGenerator : ISourceGenerator
{
    private const string AuditableEntityInterfaceName = "IAuditableEntity";

    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;

        // Identify the module name from the application project name
        var moduleName = GetModuleName(context.Compilation.AssemblyName!);
        if (moduleName == null)
        {
            context.ReportDiagnostic(Diagnostic.Create(_moduleNameNotFoundDescriptor, Location.None));
            return;
        }

        // Construct the corresponding domain project name
        var domainProjectName = $"{moduleName}.Domain";
        context.ReportDiagnostic(Diagnostic.Create(_domainProjectNameDescriptor, Location.None, domainProjectName));

        // Find the domain project in the referenced assemblies
        var domainAssemblySymbol = compilation
            .SourceModule
            .ReferencedAssemblySymbols
            .SingleOrDefault(assembly => string.Equals(assembly.Name, domainProjectName, StringComparison.Ordinal));

        if (domainAssemblySymbol == null)
        {
            context.ReportDiagnostic(Diagnostic.Create(_domainProjectNotFoundDescriptor, Location.None, domainProjectName));
            return;
        }

        var rootNs = domainAssemblySymbol.GlobalNamespace;
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
                else if (member is INamedTypeSymbol namedTypeSymbol)
                {
                    if (!ImplementsIAuditableEntity(namedTypeSymbol))
                    {
                        continue;
                    }

                    var namespaceName = namedTypeSymbol.ContainingNamespace.ToDisplayString();
                    var className = namedTypeSymbol.Name;

                    var source = GenerateDeleteEndpointCode(namespaceName, moduleName, className);
                    context.AddSource($"{className}_DeleteEndpoint.g.cs", SourceText.From(source, Encoding.UTF8));
                    context.ReportDiagnostic(Diagnostic.Create(_endpointGeneratedDescriptor, Location.None, className));
                }
            }
        }
    }

    private static bool ImplementsIAuditableEntity(INamedTypeSymbol namedTypeSymbol)
        => namedTypeSymbol
            .AllInterfaces
            .Any(i => i.Name.Contains(AuditableEntityInterfaceName));

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

    private static string GenerateDeleteEndpointCode(string namespaceName, string moduleName, string className)
    {
        var pluralClassName = Pluralize(className);
        return $@"
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

namespace {moduleName}.Application.{pluralClassName}.v1.Delete;

internal static class Endpoint
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
        [FromKeyedServices(nameof({moduleName}))] IUnitOfWork unitOfWork,
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

    private static readonly DiagnosticDescriptor _moduleNameNotFoundDescriptor = new(
        id: "SOURCEGEN001",
        title: "Module Name Not Found",
        messageFormat: "Module name not found",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _domainProjectNotFoundDescriptor = new(
        id: "SOURCEGEN002",
        title: "Domain Project Not Found",
        messageFormat: "Domain project {0} not found",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _endpointGeneratedDescriptor = new(
        id: "SOURCEGEN003",
        title: "Endpoint Generated",
        messageFormat: "Generated delete endpoint for {0}",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _domainProjectNameDescriptor = new(
        id: "SOURCEGEN004",
        title: "Domain Project Name",
        messageFormat: "Domain project name was {0}",
        category: "SourceGenerator",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);
}
