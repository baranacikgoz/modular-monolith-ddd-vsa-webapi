using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal sealed class DefaultResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        AddResponse(operation, "400", "Bad Request", context);
        AddResponse(operation, "401", "Unauthorized", context);
        AddResponse(operation, "403", "Forbidden", context);
        AddResponse(operation, "404", "Not Found", context);
        AddResponse(operation, "500", "Internal Server Error", context);
    }

    private static void AddResponse(OpenApiOperation operation, string statusCode, string description,
        OperationFilterContext context)
    {
        if ((!operation.Responses?.ContainsKey(statusCode) ?? false) && operation.Responses is not null)
        {
            operation.Responses[statusCode] = new OpenApiResponse
            {
                Description = description,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/problem+json"] = new()
                    {
                        Schema = context.SchemaGenerator.GenerateSchema(typeof(ProblemDetails),
                            context.SchemaRepository)
                    }
                }
            };
        }
    }
}
