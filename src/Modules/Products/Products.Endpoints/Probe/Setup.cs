using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Products.Endpoints.Probe;

public static class Setup
{
    public static void MapProbeEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1ProbeGroup = versionedApiGroup
            .MapGroup("/probe")
            .WithTags("Probe")
            .MapToApiVersion(1);

        v1.Endpoint.MapEndpoint(v1ProbeGroup);
    }
}
