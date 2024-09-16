using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal class RemoveDefaultResponseSchemaFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Responses.TryGetValue("204", out var value))
        {
            value.Content.Clear();

            // remove 200 response
            if (operation.Responses.TryGetValue("200", out var _))
            {
                operation.Responses.Remove("200");
            }
        }
    }
}
