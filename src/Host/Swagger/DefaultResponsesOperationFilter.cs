using Common.Core.Contracts;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

public class DefaultResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Add a default 400 Bad Request response
        operation.Responses["400"] = new OpenApiResponse
        {
            Description = "Bad Request",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(CustomProblemDetails), context.SchemaRepository)
                }
            }
        };

        operation.Responses["401"] = new OpenApiResponse
        {
            Description = "Unauthorized",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(CustomProblemDetails), context.SchemaRepository)
                }
            }
        };

        operation.Responses["403"] = new OpenApiResponse
        {
            Description = "Forbidden",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(CustomProblemDetails), context.SchemaRepository)
                }
            }
        };

        operation.Responses["404"] = new OpenApiResponse
        {
            Description = "Not Found",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(CustomProblemDetails), context.SchemaRepository)
                }
            }
        };

        operation.Responses["500"] = new OpenApiResponse
        {
            Description = "Internal Server Error",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(CustomProblemDetails), context.SchemaRepository)
                }
            }
        };
    }
}
