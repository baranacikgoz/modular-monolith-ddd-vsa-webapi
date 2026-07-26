namespace IAM.Infrastructure.Auth;

/// <summary>Forwarding scheme: routes each request to JWT or API-key auth based on which credential it carries.</summary>
internal static class MultiAuthDefaults
{
    public const string Scheme = "MultiAuth";
}
