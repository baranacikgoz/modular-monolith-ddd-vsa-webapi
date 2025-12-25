namespace Common.Application.Auth;

#pragma warning disable CA1711
public record CustomPermission(string Description, string Action, string Resource)
#pragma warning restore CA1711
{
    public string Name => NameFor(Action, Resource);

    public static string NameFor(string action, string resource)
    {
        return $"Permissions.{resource}.{action}";
    }
}
