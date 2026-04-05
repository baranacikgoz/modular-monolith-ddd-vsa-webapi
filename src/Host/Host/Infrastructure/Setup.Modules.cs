using System.Reflection;
using System.Runtime.Loader;
using Common.Application.Options;
using Common.Infrastructure.Modules;
using Host.Middlewares;
using Microsoft.AspNetCore.RateLimiting;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        var executingDir = Path.GetDirectoryName(typeof(Program).Assembly.Location) ??
                           AppDomain.CurrentDomain.BaseDirectory;
        var dllFiles = Directory.GetFiles(executingDir, "*.dll");

        using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
        var bootLogger = loggerFactory.CreateLogger(typeof(Setup).FullName!);

        foreach (var dll in dllFiles)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(dll);
                if (assemblyName.Name != null &&
                    !assemblyName.Name.StartsWith("System.", StringComparison.Ordinal) &&
                    !assemblyName.Name.StartsWith("Microsoft.", StringComparison.Ordinal))
                {
                    AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);
                }
            }
            catch (Exception ex)
            {
                var fileName = Path.GetFileName(dll);
                LoggerMessages.LogAssemblyLoadFailed(bootLogger, fileName, ex);
            }
        }

        var (activeModulesConfig, loadAll) = GetEnabledModuleNames(configuration);

        if (!loadAll && (activeModulesConfig == null || activeModulesConfig.Count == 0))
        {
            throw new InvalidOperationException(
                "Modules configuration is missing or invalid. Use '*' or an array of module names.");
        }

        var rateLimitingPolicies = new List<IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>>();
        var modulesToLoad = new List<IModule>();
        var modulesToSkip = new List<string>();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var moduleType in GetLoadableTypes(assembly))
            {
                if (typeof(IModule).IsAssignableFrom(moduleType) && !moduleType.IsInterface && !moduleType.IsAbstract)
                {
                    var module = (IModule)Activator.CreateInstance(moduleType)!;

                    if (loadAll || (activeModulesConfig != null && activeModulesConfig.Contains(module.Name)))
                    {
                        modulesToLoad.Add(module);
                    }
                    else
                    {
                        modulesToSkip.Add(module.Name);
                    }
                }
            }
        }

        if (!loadAll && activeModulesConfig != null)
        {
            var discoveredNames = modulesToLoad
                .Select(m => m.Name)
                .Concat(modulesToSkip)
                .ToHashSet(StringComparer.Ordinal);

            foreach (var requested in activeModulesConfig)
            {
                if (!discoveredNames.Contains(requested))
                {
                    LoggerMessages.LogUnknownModuleRequested(bootLogger, requested);
                }
            }
        }

        var orderedModules = modulesToLoad.OrderBy(m => m.StartupPriority).ToList();
        foreach (var module in orderedModules)
        {
            module.AddServices(services, configuration);
            services.AddSingleton(module);

            if (module.RateLimitingPolicies != null)
            {
                rateLimitingPolicies.Add(module.RateLimitingPolicies);
            }
        }

        services.AddSingleton(new ModuleRegistry(orderedModules, orderedModules.Select(m => m.Name).ToList(),
            modulesToSkip));

        services.AddCustomRateLimiting(configuration, rateLimitingPolicies.ToArray());
        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IApplicationBuilder UseModules(this WebApplication app)
    {
        var registry = app.Services.GetRequiredService<ModuleRegistry>();

        var loaded = string.Join(", ", registry.OrderedModules.Select(m => m.Name));
        LoggerMessages.LogModulesLoaded(app.Logger, loaded);

        if (registry.SkippedModuleNames.Count > 0)
        {
            var skipped = string.Join(", ", registry.SkippedModuleNames);
            LoggerMessages.LogModulesSkipped(app.Logger, skipped);
        }

        foreach (var module in registry.OrderedModules)
        {
            module.UseModule(app);
        }

        return app;
    }

    public static IApplicationBuilder MapModuleEndpoints(this WebApplication app)
    {
        var registry = app.Services.GetRequiredService<ModuleRegistry>();
        foreach (var module in registry.OrderedModules)
        {
            module.MapEndpoints(app);
        }

        return app;
    }

    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e.Types.Where(t => t != null)!;
        }
    }

    public static Assembly[] GetActiveModuleAssemblies(this IServiceCollection services)
    {
        var activeModuleNames = new HashSet<string> { "Common" };

        var registry =
            services.LastOrDefault(d => d.ServiceType == typeof(ModuleRegistry))?.ImplementationInstance as
                ModuleRegistry;
        if (registry != null)
        {
            foreach (var mod in registry.ActiveModuleNames)
            {
                activeModuleNames.Add(mod);
            }
        }

        // Pre-compute prefixes to avoid per-assembly string allocations in the filter loop.
        var activePrefixes = activeModuleNames.Select(m => m + ".").ToList();

        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a =>
            {
                var name = a.GetName().Name;
                if (name == null)
                {
                    return false;
                }

                return activeModuleNames.Contains(name) ||
                       activePrefixes.Exists(prefix => name.StartsWith(prefix, StringComparison.Ordinal));
            })
            .ToArray();
    }

    private static (IReadOnlyList<string>? Names, bool LoadAll) GetEnabledModuleNames(IConfiguration configuration)
    {
        var overrideModule = configuration["TestModuleOverride"];
        IReadOnlyList<string>? names;

        if (!string.IsNullOrWhiteSpace(overrideModule))
        {
            names = overrideModule.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            names = configuration.GetSection(nameof(ModulesOptions)).Get<ModulesOptions>()?.EnabledModules;
        }

        var loadAll = names != null && names.Contains("*");
        return (names, loadAll);
    }

    private sealed record ModuleRegistry(
        IReadOnlyList<IModule> OrderedModules,
        IReadOnlyList<string> ActiveModuleNames,
        IReadOnlyList<string> SkippedModuleNames);

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Information, Message = "Loaded modules: [{LoadedModules}]")]
        public static partial void LogModulesLoaded(ILogger logger, string loadedModules);

        [LoggerMessage(Level = LogLevel.Information, Message = "Skipped modules: [{SkippedModules}]")]
        public static partial void LogModulesSkipped(ILogger logger, string skippedModules);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "Failed to load assembly '{AssemblyFileName}', skipping. This may indicate a corrupt or incompatible module binary.")]
        public static partial void LogAssemblyLoadFailed(ILogger logger, string assemblyFileName, Exception ex);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "Module '{ModuleName}' was requested in configuration but no IModule implementation with that name was found. Check for typos.")]
        public static partial void LogUnknownModuleRequested(ILogger logger, string moduleName);
    }
}
