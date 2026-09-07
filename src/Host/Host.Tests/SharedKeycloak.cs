using Common.Tests;
using Testcontainers.Keycloak;

namespace Host.Tests;

/// <summary>
///     One Keycloak per test process, shared by every <see cref="HostTestFactory" /> (several tests boot their own
///     host). Started lazily on first use; Testcontainers' reaper removes it when the process exits.
/// </summary>
internal static class SharedKeycloak
{
    public const string Image = "quay.io/keycloak/keycloak:26.7";

    private static readonly Lazy<Task<KeycloakContainer>> Instance =
        new(StartAsync, LazyThreadSafetyMode.ExecutionAndPublication);

    public static Task<KeycloakContainer> GetAsync()
    {
        return Instance.Value;
    }

    private static async Task<KeycloakContainer> StartAsync()
    {
        var container = new KeycloakBuilder(Image).WithRealm(TestPaths.RealmFile).Build();
        await container.StartAsync();
        return container;
    }
}
