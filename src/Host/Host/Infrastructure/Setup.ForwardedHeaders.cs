using Common.Application.Options;
using Microsoft.AspNetCore.HttpOverrides;
using IPNetwork = System.Net.IPNetwork;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IServiceCollection AddCustomForwardedHeaders(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var proxyOptions = configuration
                               .GetSection(nameof(ReverseProxyOptions))
                               .Get<ReverseProxyOptions>()
                           ?? new ReverseProxyOptions();

        if (!proxyOptions.IsEnabled)
        {
            return services;
        }

        services.Configure<ForwardedHeadersOptions>(o =>
        {
            o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            o.ForwardLimit = proxyOptions.ForwardLimit;

            if (proxyOptions.TrustedNetworks.Count > 0)
            {
                o.KnownIPNetworks.Clear();
                o.KnownProxies.Clear();

                foreach (var cidr in proxyOptions.TrustedNetworks)
                {
                    o.KnownIPNetworks.Add(IPNetwork.Parse(cidr));
                }
            }
        });

        return services;
    }
}
