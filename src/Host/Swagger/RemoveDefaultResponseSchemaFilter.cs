using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal class RemoveDefaultResponseSchemaFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Responses?.TryGetValue("204", out var value) ?? false)
        {
            value.Content?.Clear();

            operation.Responses.Remove("200", out var _);
        }
    }
}
