using System.Reflection;

namespace Common.Tests.Architecture;

/// <summary>
/// Discovers every module and shared-kernel assembly present in this test run's output
/// directory, so architecture tests scale to a new module automatically instead of hardcoding
/// module name lists. (<see cref="ModuleBoundaryTests"/> used to hardcode
/// <c>["IAM", "Products", "Notifications"]</c> and silently missed any new module.)
/// </summary>
internal static class SolutionAssemblies
{
    private static readonly string[] _excludedPrefixes =
    [
        "System.", "Microsoft.", "netstandard", "testhost", "xunit", "NetArchTest",
        "NSubstitute", "Bogus", "Castle.", "Newtonsoft", "Npgsql", "MassTransit",
        "Serilog", "OpenTelemetry", "FluentValidation", "Quartz", "Hangfire",
        "Respawn", "Testcontainers", "Docker", "Polly", "StackExchange", "Aigamo",
        "SonarAnalyzer", "coverlet", "DotNet", "Asp.Versioning", "Swashbuckle", "DnsClient",
    ];

    public static IReadOnlyList<Assembly> All { get; } = Discover();

    public static IReadOnlyList<Assembly> DomainAssemblies { get; } =
        All.Where(a => a.GetName().Name!.EndsWith(".Domain", StringComparison.Ordinal)).ToList();

    /// <summary>Every module name that owns at least one discovered assembly, excluding the shared kernel (Common) and the composition root (Host).</summary>
    public static IReadOnlyList<string> ModuleNames { get; } = All
        .Select(a => a.GetName().Name!.Split('.')[0])
        .Distinct(StringComparer.Ordinal)
        .Where(m => m is not ("Common" or "Host"))
        .OrderBy(m => m, StringComparer.Ordinal)
        .ToList();

    private static List<Assembly> Discover()
    {
        var assemblies = new List<Assembly>();

        foreach (var dllPath in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.dll"))
        {
            var name = Path.GetFileNameWithoutExtension(dllPath);

            if (_excludedPrefixes.Any(p => name.StartsWith(p, StringComparison.OrdinalIgnoreCase))
                || name.EndsWith(".Tests", StringComparison.Ordinal))
            {
                continue;
            }

            try
            {
                // Load by simple name (not by path): these assemblies are all part of this
                // test project's own dependency closure, so the default AssemblyLoadContext
                // resolves them from the same probing paths/deps.json Assembly.LoadFrom would
                // have read from disk directly.
                assemblies.Add(Assembly.Load(name));
            }
            catch (Exception ex) when (ex is BadImageFormatException or FileLoadException or FileNotFoundException)
            {
                // Native/mixed-mode dependency, or not actually part of this app's closure.
            }
        }

        return assemblies;
    }
}
