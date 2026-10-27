using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Inventory.Domain.WarehouseConnections;
using Microsoft.Extensions.Options;
using Polly;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     Per-connection resilience for warehouse calls. Each connection gets its own pipeline (key
///     <c>Warehouse{Provider}:{connectionId}</c>) built from the profile of its provider in
///     <c>ResiliencyOptions.Keyed["Warehouse{Provider}"]</c>, because providers publish different quotas and a store's burst
///     or outage must not spend the quota or open the circuit of any other store behind the same HTTP client. The HTTP
///     client itself carries no resilience handler (see <see cref="Setup" />).
///     <para>
///         The pipeline is a rate limiter, a circuit breaker and an attempt timeout, and deliberately has no retry
///         strategy. That is the choice that keeps a non-idempotent call safe: a release (or any call that creates something at
///         the warehouse) whose first attempt failed is never sent again by the pipeline, so a timeout cannot become a double
///         release. Resolving an ambiguous failure is the caller's job, through the idempotent reconcile call.
///     </para>
/// </summary>
internal sealed class WarehouseResiliencePipelines(KeyedResiliencePipelines pipelines, IOptions<ResiliencyOptions> options)
{
    private const string ProfilePrefix = "Warehouse";

    public static string ProfileNameOf(WarehouseProvider provider)
    {
        return ProfilePrefix + provider;
    }

    public static string KeyOf(WarehouseConnection connection)
    {
        return $"{ProfileNameOf(connection.Provider)}:{connection.Id}";
    }

    public ResiliencePipeline<HttpResponseMessage> For(WarehouseConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        var profileName = ProfileNameOf(connection.Provider);
        if (!options.Value.Keyed.TryGetValue(profileName, out var profile))
        {
            throw new InvalidOperationException($"No keyed resilience profile named '{profileName}' in ResiliencyOptions.Keyed.");
        }

        return pipelines.GetOrAdd(KeyOf(connection), profile);
    }
}
