using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Common.Tests;

/// <summary>
///     Registered into a plain <see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> by
///     unit tests exercising registration-time Production guards (which resolve the environment via the
///     <see cref="IHostEnvironment"/> descriptor). Injecting this instead of mutating the process-wide
///     ASPNETCORE_ENVIRONMENT variable keeps those tests safe to run in parallel with integration-test
///     factory boots in the same assembly.
/// </summary>
public sealed class FakeHostEnvironment(string environmentName) : IHostEnvironment
{
    public string EnvironmentName { get; set; } = environmentName;
    public string ApplicationName { get; set; } = "Tests";
    public string ContentRootPath { get; set; } = string.Empty;
    public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
}
