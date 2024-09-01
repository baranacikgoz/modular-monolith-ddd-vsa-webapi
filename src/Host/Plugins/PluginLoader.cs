using Common.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Composition.Hosting;
using System.Reflection;
using System.Windows.Input;

namespace Host.Plugins;

public static class PluginLoader
{
#pragma warning disable
    public static IEnumerable<IModule> LoadModules(IWebHostEnvironment environment)
    {
        var asd = Path.GetFullPath(Path.GetDirectoryName(typeof(Program).Assembly.Location));
        var asdf = new AssemblyName(Path.GetFileNameWithoutExtension(asd));
        var hostDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var srcDirectory = Directory.GetParent(hostDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;

        if (srcDirectory == null)
        {
            Console.WriteLine("Could not locate src directory.");
            yield break;
        }

        var modulesDirectory = Path.Combine(srcDirectory, "Modules");
        Console.WriteLine($"Looking for modules in: {modulesDirectory}");

        if (!Directory.Exists(modulesDirectory))
        {
            Console.WriteLine("Modules directory not found!");
            yield break;
        }

        var moduleProjects = Directory.GetDirectories(modulesDirectory);
        var assemblyPaths = new List<string>();

        foreach (var projectPath in moduleProjects)
        {
            var projectName = new DirectoryInfo(projectPath).Name; // For Single project modules such as "BackgroundJobs"
            var infrastructureProjectPath = Path.Combine(projectPath, $"{projectName}.Infrastructure"); // For Multi project modules such as "IAM"

            // If it's a multi project module
            if (Directory.Exists(infrastructureProjectPath))
            {
                try
                {
                    Console.WriteLine($"Adding assembly: {infrastructureProjectPath}");
                    assemblyPaths.Add(Path
                        .Combine(infrastructureProjectPath, "bin", "debug", "net8.0", $"{projectName}.Infrastructure.dll")
                        .Replace('\\', Path.DirectorySeparatorChar));
                    Console.WriteLine($"Added assembly: {infrastructureProjectPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding {infrastructureProjectPath}: {ex.Message}");
                }
                continue;
            }

            // If it's a single project module
            Console.WriteLine($"Adding assembly: {projectPath}");
            assemblyPaths.Add(Path.Combine(projectPath, "bin", "debug", "net8.0", $"{projectName}.dll").Replace('\\', Path.DirectorySeparatorChar));
            Console.WriteLine($"Added assembly: {projectPath}");
        }

        foreach(var assemblyPath in assemblyPaths)
        {
            var pluginLoadContext = new PluginLoadContext(assemblyPath);
            var assembly = pluginLoadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyPath)));

            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IModule).IsAssignableFrom(type) && !type.IsInterface)
                {
                    var module = (IModule)Activator.CreateInstance(type);
                    yield return module;
                }
            }
        }
    }
}
