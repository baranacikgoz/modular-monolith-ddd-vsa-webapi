using System.Collections.Frozen;
using Common.Application.Auth;

namespace IAM.Application.Auth;

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

        new("Create My StoreProduct", CustomActions.CreateMy, CustomResources.StoreProducts),
        new("Read My StoreProduct", CustomActions.ReadMy, CustomResources.StoreProducts),
        new("Update My StoreProduct", CustomActions.UpdateMy, CustomResources.StoreProducts),
        new("Delete My StoreProduct", CustomActions.DeleteMy, CustomResources.StoreProducts),

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

        // In this marketplace system, the products those store owners can sell are managed by the system admins
        // You may think of this as Trendyol, Hepsiburada, and sellers can only sell the products that are predefined by the system admins (then customize prices and etc. see ->StoreProduct entity).
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