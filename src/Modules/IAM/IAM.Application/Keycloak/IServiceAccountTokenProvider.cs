namespace IAM.Application.Keycloak;

/// <summary>Client-credentials token of the resource client's service account, cached until shortly before expiry.</summary>
public interface IServiceAccountTokenProvider
{
    Task<string> GetAccessTokenAsync(bool forceRefresh, CancellationToken cancellationToken);
}
