using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.Logging;
using Common.Application.Options;
using Common.Infrastructure.Modules;
using Host.Middlewares;
using Microsoft.AspNetCore.RateLimiting;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Host.Infrastructure;

internal static partial class Setup
{
    private sealed record ModuleRegistry(IReadOnlyList<string> ActiveModuleNames, IReadOnlyList<string> SkippedModuleNames);

    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        var executingDir = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? AppDomain.CurrentDomain.BaseDirectory;
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
                LoggerMessages.LogAssemblyLoadFailed(bootLogger, Path.GetFileName(dll), ex);
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

        services.AddSingleton(new ModuleRegistry(orderedModules.Select(m => m.Name).ToList(), modulesToSkip));

        services.AddCustomRateLimiting(configuration, rateLimitingPolicies.ToArray());
        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IApplicationBuilder UseModules(this WebApplication app)
    {
        var modules = app.Services.GetServices<IModule>().OrderBy(m => m.StartupPriority).ToList();
        var registry = app.Services.GetRequiredService<ModuleRegistry>();
        
        var loaded = string.Join(", ", modules.Select(m => m.Name));
        LoggerMessages.LogModulesLoaded(app.Logger, loaded);

        if (registry.SkippedModuleNames.Count > 0)
        {
            var skipped = string.Join(", ", registry.SkippedModuleNames);
            LoggerMessages.LogModulesSkipped(app.Logger, skipped);
        }

        foreach (var module in modules)
        {
            module.UseModule(app);
        }

        return app;
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Information, Message = "Loaded modules: [{LoadedModules}]")]
        public static partial void LogModulesLoaded(ILogger logger, string loadedModules);

        [LoggerMessage(Level = LogLevel.Information, Message = "Skipped modules: [{SkippedModules}]")]
        public static partial void LogModulesSkipped(ILogger logger, string skippedModules);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Failed to load assembly '{AssemblyFileName}', skipping.")]
        public static partial void LogAssemblyLoadFailed(ILogger logger, string assemblyFileName, Exception ex);
    }

    public static IApplicationBuilder MapModuleEndpoints(this WebApplication app)
    {
        var modules = app.Services.GetServices<IModule>().OrderBy(m => m.StartupPriority);
        foreach (var module in modules)
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

        var registry = services.LastOrDefault(d => d.ServiceType == typeof(ModuleRegistry))?.ImplementationInstance as ModuleRegistry;
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
}
