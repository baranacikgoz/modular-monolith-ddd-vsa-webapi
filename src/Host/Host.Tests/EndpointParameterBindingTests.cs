using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Host.Tests;

// A mapped handler whose Request type declares binding sources on its members ([FromBody], [FromRoute], [FromQuery],
// ...) must take it as [AsParameters]. Without the attribute the minimal API reads the whole JSON body as the Request
// type, so the real client body ({ "value": ... }) is refused with 400 ("could not be mapped to any .NET member")
// while a test that serializes the Request wrapper itself still passes.
[Collection("Host")]
public class EndpointParameterBindingTests(HostTestFactory factory)
{
    [Fact]
    public void MappedHandlers_WithBindingSourcesOnTheirRequestType_DeclareAsParameters()
    {
        var handlers = factory.Services.GetRequiredService<EndpointDataSource>().Endpoints
            .Select(e => e.Metadata.GetMetadata<MethodInfo>())
            .OfType<MethodInfo>()
            .ToList();

        Assert.NotEmpty(handlers);
        var offenders = handlers
            .SelectMany(handler => handler.GetParameters()
                .Where(p => !HasBindingSource(p) && !p.IsDefined(typeof(AsParametersAttribute), inherit: false))
                .Where(p => p.ParameterType.GetProperties().Any(HasBindingSource))
                .Select(p => $"{handler.DeclaringType?.FullName}.{handler.Name}: parameter '{p.Name}' of type {p.ParameterType.FullName}"))
            .Distinct(StringComparer.Ordinal)
            .Order(StringComparer.Ordinal)
            .ToList();

        Assert.True(offenders.Count == 0,
            $"Mapped handlers take a Request type with binding sources without [AsParameters]: {string.Join("; ", offenders)}.");
    }

    private static bool HasBindingSource(ICustomAttributeProvider member)
    {
        return member.GetCustomAttributes(inherit: true).OfType<IBindingSourceMetadata>().Any();
    }
}
