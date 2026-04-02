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
        var executingDir = AppDomain.CurrentDomain.BaseDirectory;
        var dllFiles = Directory.GetFiles(executingDir, "*.dll");

        foreach (var dll in dllFiles)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(dll);
                AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);
            }
            catch
            {
                // Ignored
            }
        }

        var overrideModule = configuration.GetValue<string>("TestModuleOverride");
        IReadOnlyList<string>? activeModulesConfig = overrideModule != null 
            ? overrideModule.Split(',') 
            : configuration.GetSection(nameof(ModulesOptions)).Get<ModulesOptions>()?.EnabledModules;
            
        var loadAll = activeModulesConfig != null && activeModulesConfig.Contains("*");

        if (!loadAll && activeModulesConfig == null)
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
        var registry = (ModuleRegistry?)services.LastOrDefault(d => d.ServiceType == typeof(ModuleRegistry))?.ImplementationInstance;
        var activeModuleNames = registry?.ActiveModuleNames.ToHashSet() ?? new HashSet<string>();
        activeModuleNames.Add("Common");

        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a =>
            {
                var name = a.GetName().Name;
                if (name == null)
                {
                    return false;
                }

                return activeModuleNames.Contains(name) ||
                       activeModuleNames.Any(m => name.StartsWith(m + ".", StringComparison.Ordinal));
            })
            .ToArray();
    }
}
