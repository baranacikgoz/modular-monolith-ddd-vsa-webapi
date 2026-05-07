using System.Reflection;
using Common.IntegrationEvents;
using NetArchTest.Rules;
using Xunit;

namespace Common.Tests.Architecture;

#pragma warning disable CA1515 // Consider making public types internal

/// <summary>
/// Architecture boundary tests that enforce module isolation rules.
/// These tests turn CLAUDE.md / GEMINI.md convention-only rules into failing CI checks.
/// </summary>
public sealed class ModuleBoundaryTests
{
    // Modules that have a dedicated .Domain assembly.
    // BackgroundJobs and Outbox are single-project modules with no Domain split.
    private static readonly string[] DomainModules =
    [
        "IAM",
        "Products",
        "Notifications",
    ];

    // Modules that have a dedicated .Application assembly.
    private static readonly string[] ApplicationModules =
    [
        "IAM",
        "Products",
        "Notifications",
    ];

    /// <summary>
    /// No Domain assembly may take a compile-time dependency on any other module's namespace.
    /// Rule source: CLAUDE.md — "No module .csproj may reference another module .csproj."
    /// </summary>
    [Fact]
    public void ModuleDomain_MustNotDependOn_OtherModules()
    {
        foreach (var module in DomainModules)
        {
            var assembly = Assembly.Load($"{module}.Domain");

            // All other module root namespaces (both Domain and single-project modules)
            var forbidden = DomainModules
                .Except([module])
                .Select(m => $"{m}.")
                .Concat(["BackgroundJobs.", "Outbox."])
                .ToArray();

            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(forbidden)
                .GetResult();

            Assert.True(
                result.IsSuccessful,
                $"{module}.Domain has forbidden cross-module dependencies: "
                + string.Join(", ", result.FailingTypes?.Select(t => t.FullName) ?? []));
        }
    }

    /// <summary>
    /// IntegrationEvent subclasses must live in Common.IntegrationEvents, never inside a module assembly.
    /// Rule source: CLAUDE.md — "Defined in src/Common/Common.IntegrationEvents/{SourceModule}.cs"
    /// </summary>
    [Fact]
    public void IntegrationEvents_MustLiveIn_CommonIntegrationEvents()
    {
        // Collect all module assemblies that could illegally define IntegrationEvents
        var moduleAssemblies = ApplicationModules
            .SelectMany<string, Assembly>(m =>
            [
                Assembly.Load($"{m}.Domain"),
                Assembly.Load($"{m}.Application"),
            ])
            .Append(Assembly.Load("BackgroundJobs"))
            .Append(Assembly.Load("Outbox"))
            .ToList();

        var result = Types
            .InAssemblies(moduleAssemblies)
            .That()
            .AreClasses()
            .And()
            .Inherit(typeof(IntegrationEvent))
            .ShouldNot()
            .ResideInNamespaceMatching(".*")   // i.e. any namespace in these assemblies is wrong
            .GetResult();

        Assert.True(
            result.IsSuccessful,
            "IntegrationEvent subclasses found outside Common.IntegrationEvents: "
            + string.Join(", ", result.FailingTypes?.Select(t => t.FullName) ?? []));
    }
}

#pragma warning restore CA1515
