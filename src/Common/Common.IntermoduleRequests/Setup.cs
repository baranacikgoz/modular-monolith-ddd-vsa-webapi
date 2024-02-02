using Common.IntermoduleRequests.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Common.IntermoduleRequests;

public static class Setup
{
    public static IServiceCollection AddIntermoduleRequests(this IServiceCollection services)
        => services.AddSingleton(typeof(IIntermoduleRequestClient<,>), typeof(MassTransitIntermoduleRequestClient<,>));

}
