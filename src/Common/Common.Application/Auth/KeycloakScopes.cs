namespace Common.Application.Auth;

/// <summary>
///     Authorization scopes as declared in Keycloak (<c>resource:action</c>, ownership variants end in <c>-own</c>).
///     Ownership itself is still enforced in the query (<c>OwnerId == currentUser.Id</c>); the scope only says
///     the caller may operate on resources they own.
/// </summary>
public static class KeycloakScopes
{
    public static class Users
    {
        public const string View = "users:view";
        public const string Search = "users:search";
        public const string ViewOwn = "users:view-own";
    }

    public static class Sessions
    {
        public const string ViewOwn = "sessions:view-own";
        public const string RevokeOwn = "sessions:revoke-own";
    }

    public static class Devices
    {
        public const string UpdateOwn = "devices:update-own";
    }

    public static class Stores
    {
        public const string Create = "stores:create";
        public const string View = "stores:view";
        public const string Search = "stores:search";
        public const string Update = "stores:update";
        public const string CreateOwn = "stores:create-own";
        public const string ViewOwn = "stores:view-own";
        public const string UpdateOwn = "stores:update-own";
    }

    public static class Products
    {
        public const string Create = "products:create";
        public const string View = "products:view";
        public const string Search = "products:search";
        public const string Update = "products:update";
        public const string Delete = "products:delete";
        public const string CreateOwn = "products:create-own";
        public const string ViewOwn = "products:view-own";
        public const string UpdateOwn = "products:update-own";
        public const string DeleteOwn = "products:delete-own";
        public const string SearchOwn = "products:search-own";
    }

    public static class ProductTemplates
    {
        public const string Create = "product-templates:create";
        public const string View = "product-templates:view";
        public const string Search = "product-templates:search";
        public const string Update = "product-templates:update";
    }

    public static class Hangfire
    {
        public const string Manage = "hangfire:manage";
    }
}
