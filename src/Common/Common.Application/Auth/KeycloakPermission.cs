namespace Common.Application.Auth;

/// <summary>
///     A Keycloak Authorization Services permission request: one resource and one scope on it.
///     Scopes are named <c>{resource}:{action}</c> (see <see cref="KeycloakScopes" />), so the resource is
///     always the prefix and callers only ever pass the scope. Serialized as the ASP.NET Core policy name
///     <c>{resource}#{scope}</c>, the same shape the Keycloak token endpoint accepts in its <c>permission</c> parameter.
/// </summary>
public readonly record struct KeycloakPermission(string Resource, string Scope)
{
    public const char Separator = '#';
    public const char ScopeSeparator = ':';

    /// <summary>Builds the permission for a <c>{resource}:{action}</c> scope.</summary>
    public static KeycloakPermission FromScope(string scope)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scope);

        var separatorIndex = scope.IndexOf(ScopeSeparator, StringComparison.Ordinal);
        if (separatorIndex <= 0 || separatorIndex == scope.Length - 1)
        {
            throw new ArgumentException($"Scope '{scope}' must be named '{{resource}}:{{action}}'.", nameof(scope));
        }

        return new KeycloakPermission(scope[..separatorIndex], scope);
    }

    public static string PolicyName(string resource, string scope)
    {
        return $"{resource}{Separator}{scope}";
    }

    public string PolicyName()
    {
        return PolicyName(Resource, Scope);
    }

    public static bool TryParse(string? policyName, out KeycloakPermission permission)
    {
        permission = default;
        if (string.IsNullOrEmpty(policyName))
        {
            return false;
        }

        var separatorIndex = policyName.IndexOf(Separator, StringComparison.Ordinal);
        if (separatorIndex <= 0 || separatorIndex == policyName.Length - 1 ||
            policyName.IndexOf(Separator, separatorIndex + 1) >= 0)
        {
            return false;
        }

        permission = new KeycloakPermission(policyName[..separatorIndex], policyName[(separatorIndex + 1)..]);
        return true;
    }
}
