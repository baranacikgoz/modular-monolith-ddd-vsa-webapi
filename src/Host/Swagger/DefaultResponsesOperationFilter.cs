using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal sealed class DefaultResponsesOperationFilter : IOperationFilter
{
    private static OpenApiResponse CreateErrorResponse(string description, OperationFilterContext context)
    {
        return new OpenApiResponse
        {
            Description = description,
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/problem+json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(ProblemDetails), context.SchemaRepository)
                }
            }
        };
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses["400"] = CreateErrorResponse("Bad Request", context);
        operation.Responses["401"] = CreateErrorResponse("Unauthorized", context);
        operation.Responses["403"] = CreateErrorResponse("Forbidden", context);
        operation.Responses["404"] = CreateErrorResponse("Not Found", context);
        operation.Responses["500"] = CreateErrorResponse("Internal Server Error", context);
    }
}
