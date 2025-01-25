using System.Reflection;
using Common.Infrastructure.CQS.PipelineBehaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.CQS;

public static class Setup
{
    public static IServiceCollection AddCommonCommandsQueriesHandlers(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            //cfg.AddBehavior<PingPongBehavior>()
            //cfg.AddStreamBehavior<PingPongStreamBehavior>()
            //cfg.AddRequestPreProcessor<PingPreProcessor>()
            //cfg.AddRequestPostProcessor<PingPongPostProcessor>()

            cfg.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });
}
