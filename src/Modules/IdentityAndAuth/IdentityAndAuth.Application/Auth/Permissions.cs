using System.Collections.Frozen;
using Common.Application.Auth;

namespace IdentityAndAuth.Application.Auth;

public static class CustomPermissions
{
    private static readonly HashSet<CustomPermission> _basic =
    [
        new("Read My Profile", CustomActions.ReadMy, CustomResources.Users),
        new("Update My Profile", CustomActions.UpdateMy, CustomResources.Users),

        new("Create My Store", CustomActions.CreateMy, CustomResources.Stores),
        new("Read My Store", CustomActions.ReadMy, CustomResources.Stores),
        new("Update My Store", CustomActions.UpdateMy, CustomResources.Stores),
        new("Delete My Store", CustomActions.DeleteMy, CustomResources.Stores),

        new("Create My Product", CustomActions.CreateMy, CustomResources.Products),
        new("Read My Product", CustomActions.ReadMy, CustomResources.Products),
        new("Update My Product", CustomActions.UpdateMy, CustomResources.Products),
        new("Delete My Product", CustomActions.DeleteMy, CustomResources.Products),

        // For individual users to see all stores and products
        new("Read Products", CustomActions.Read, CustomResources.Products),
    ];

    private static readonly HashSet<CustomPermission> _systemAdmin =
    [
        new("Create Users", CustomActions.Create, CustomResources.Users),
        new("Read Users", CustomActions.Read, CustomResources.Users),
        new("Update Users", CustomActions.Update, CustomResources.Users),
        new("Delete Users", CustomActions.Delete, CustomResources.Users),

        new("Create Stores", CustomActions.Create, CustomResources.Stores),
        new("Read Stores", CustomActions.Read, CustomResources.Stores),
        new("Update Stores", CustomActions.Update, CustomResources.Stores),
        new("Delete Stores", CustomActions.Delete, CustomResources.Stores),

        new("Create Products", CustomActions.Create, CustomResources.Products),
        new("Read Products", CustomActions.Read, CustomResources.Products),
        new("Update Products", CustomActions.Update, CustomResources.Products),
        new("Delete Products", CustomActions.Delete, CustomResources.Products),
    ];

    public static readonly IReadOnlySet<string> Basic =
        _basic
        .Select(r => r.Name)
        .ToFrozenSet();
    public static readonly IReadOnlySet<string> SystemAdmin =
        _basic
        .Concat(_systemAdmin)
        .Select(r => r.Name)
        .ToFrozenSet();
}
