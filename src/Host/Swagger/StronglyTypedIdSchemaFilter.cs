using Common.Domain.StronglyTypedIds;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

public class StronglyTypedIdSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var typeInfo = context.Type;
        if (typeof(IStronglyTypedId).IsAssignableFrom(typeInfo))
        {
            schema.Type = "string";
            schema.Format = "uuid";
            schema.Properties?.Clear();
        }
    }
}
