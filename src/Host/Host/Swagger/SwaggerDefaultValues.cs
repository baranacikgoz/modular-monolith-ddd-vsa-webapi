using System.Globalization;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal sealed class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;
        operation.Deprecated |= apiDescription.IsDeprecated();

        if (operation.Responses != null)
        {
            foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse
                    ? "default"
                    : responseType.StatusCode.ToString(CultureInfo.InvariantCulture);

                if (!operation.Responses.TryGetValue(responseKey, out var response))
                {
                    continue;
                }

                foreach (var contentType in response.Content?.Keys.ToList() ?? [])
                {
                    if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                    {
                        response.Content?.Remove(contentType);
                    }
                }
            }
        }

        if (operation.Parameters == null)
        {
            return;
        }

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);

            if (description == null)
            {
                continue;
            }

            // 2. Cast to Concrete Class (OpenApiParameter)
            // Microsoft.OpenApi v2 interfaces (IOpenApiParameter) often do not have setters.
            if (parameter is not OpenApiParameter concreteParameter)
            {
                continue;
            }

            concreteParameter.Description ??= description.ModelMetadata?.Description;

            // 3. Fix Schema Default (IOpenApiAny -> JsonNode)
            if (concreteParameter.Schema is OpenApiSchema concreteSchema &&
                concreteSchema.Default == null &&
                description.DefaultValue != null &&
                description.DefaultValue is not DBNull &&
                description.ModelMetadata is { } _)
            {
                concreteSchema.Default = MapToJsonNode(description.DefaultValue);
            }

            concreteParameter.Required |= description.IsRequired;
        }
    }

    /// <summary>
    ///     Microsoft.OpenApi v2 replaces IOpenApiAny with System.Text.Json.Nodes.JsonNode
    /// </summary>
    private static JsonValue? MapToJsonNode(object? value)
    {
        if (value == null)
        {
            return null;
        }

        return value switch
        {
            bool b => JsonValue.Create(b),
            int i => JsonValue.Create(i),
            long l => JsonValue.Create(l),
            float f => JsonValue.Create(f),
            double d => JsonValue.Create(d),
            string s => JsonValue.Create(s),
            DateTime dt => JsonValue.Create(dt),
            DateTimeOffset dto => JsonValue.Create(dto),
            Guid g => JsonValue.Create(g),
            _ => JsonValue.Create(value.ToString())
        };
    }
}
