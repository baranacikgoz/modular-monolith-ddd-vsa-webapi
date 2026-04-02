using Asp.Versioning;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Endpoints.Versioning;

public static class Setup
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        return services
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = false;
            })
            .Services;
    }

    public static ApiVersionSet GetApiVersionSet(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .NewApiVersionSet()
            .HasApiVersion(1)
            // .HasApiVersion(2)
            // .HasApiVersion(3)
            .Build();
    }
}
