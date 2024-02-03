using Common.InterModuleRequests.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Common.InterModuleRequests;

public static class Setup
{
    public static IServiceCollection AddInterModuleRequests(this IServiceCollection services)
        => services.AddSingleton(typeof(IInterModuleRequestClient<,>), typeof(MassTransitInterModuleRequestClient<,>));

}
