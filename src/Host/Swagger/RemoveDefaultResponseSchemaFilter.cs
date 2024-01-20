using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

public class RemoveDefaultResponseSchemaFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Responses.TryGetValue("200", out var value) && ShouldHaveEmptyResponseBody(context))
        {
            value.Content.Clear();
        }
    }

    private static bool ShouldHaveEmptyResponseBody(OperationFilterContext context)
    {
        // check if endpoint is registered with "api.Produces<IEmpty200Response>()"
        return context
            .ApiDescription
            .SupportedResponseTypes
            .Any(x => x.Type == typeof(IEmpty200Response));
    }
}
