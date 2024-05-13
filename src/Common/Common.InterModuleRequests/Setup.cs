using Common.InterModuleRequests.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Common.InterModuleRequests;
public static class Setup
{
    public static IServiceCollection AddCommonInterModuleRequests(this IServiceCollection services)
        => services.AddTransient(typeof(IInterModuleRequestClient<,>), typeof(MassTransitInterModuleRequestClient<,>));

}
