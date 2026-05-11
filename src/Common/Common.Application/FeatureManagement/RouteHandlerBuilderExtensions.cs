using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.Application.FeatureManagement;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder RequireFeature(this RouteHandlerBuilder builder, string featureName)
    {
        return builder.AddEndpointFilter(new RequireFeatureFilter(featureName));
    }
}
