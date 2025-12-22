using Common.Domain.StronglyTypedIds;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Host.Swagger;

internal sealed class StronglyTypedIdSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema concreteSchema)
        {
            return;
        }

        if (typeof(IStronglyTypedId).IsAssignableFrom(context.Type))
        {
            concreteSchema.Type = JsonSchemaType.String;
            concreteSchema.Format = "uuid";
            concreteSchema.Properties?.Clear();
        }
    }
}
