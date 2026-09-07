using System.Net.Http.Json;
using System.Text.Json;

namespace IAM.Infrastructure.Keycloak;

internal static class HttpContentJsonExtensions
{
    /// <summary>
    ///     Reads a Keycloak error body that may not be JSON at all (proxy error page, truncated response under
    ///     load). A malformed body is not worth failing the request over: the status code already carries the
    ///     decision, the body only adds detail for logging.
    /// </summary>
    public static async Task<T?> TryReadFromJsonAsync<T>(this HttpContent content, CancellationToken cancellationToken)
        where T : class
    {
        try
        {
            return await content.ReadFromJsonAsync<T>(cancellationToken);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
