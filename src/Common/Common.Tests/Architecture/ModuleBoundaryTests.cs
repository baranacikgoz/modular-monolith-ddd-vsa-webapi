using Common.IntegrationEvents;
using NetArchTest.Rules;
using Xunit;

namespace Common.Tests.Architecture;

#pragma warning disable CA1515 // Consider making public types internal

/// <summary>
/// Architecture boundary tests that enforce module isolation rules.
/// These tests turn CLAUDE.md convention-only rules into failing CI checks.
/// </summary>
public sealed class ModuleBoundaryTests
{
    /// <summary>
    /// No Domain assembly may take a compile-time dependency on any other module's namespace.
    /// Rule source: CLAUDE.md: "No module .csproj may reference another module .csproj."
    /// </summary>
    [Fact]
    public void ModuleDomain_MustNotDependOn_OtherModules()
    {
        foreach (var assembly in SolutionAssemblies.DomainAssemblies)
        {
            var module = assembly.GetName().Name!.Split('.')[0];

            // All other module root namespaces (both Domain-split and single-project modules).
            var forbidden = SolutionAssemblies.ModuleNames
                .Except([module])
                .Select(m => $"{m}.")
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
    /// Rule source: CLAUDE.md: "Defined in src/Common/Common.IntegrationEvents/{SourceModule}.cs"
    /// </summary>
    [Fact]
    public void IntegrationEvents_MustLiveIn_CommonIntegrationEvents()
    {
        // Every discovered assembly outside the shared kernel (Common.IntegrationEvents is the
        // one legitimate home for an IntegrationEvent, and it lives in the "Common" module).
        var moduleAssemblies = SolutionAssemblies.All
            .Where(a => a.GetName().Name!.Split('.')[0] != "Common")
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
