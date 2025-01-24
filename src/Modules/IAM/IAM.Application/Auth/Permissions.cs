using System.Collections.Frozen;
using Common.Application.Auth;

namespace IAM.Application.Auth;

public static class CustomPermissions
{
    private static readonly HashSet<CustomPermission> _basic =
    [
        new("Read My Profile", CustomActions.ReadMy, CustomResources.ApplicationUsers),
        new("Update My Profile", CustomActions.UpdateMy, CustomResources.ApplicationUsers),

        new("Create My Store", CustomActions.CreateMy, CustomResources.Stores),
        new("Read My Store", CustomActions.ReadMy, CustomResources.Stores),
        new("Read Stores", CustomActions.Read, CustomResources.Stores), // For individual users to see all stores
        new("Search Stores", CustomActions.Search, CustomResources.Stores),
        new("Update My Store", CustomActions.UpdateMy, CustomResources.Stores),
        new("Delete My Store", CustomActions.DeleteMy, CustomResources.Stores),

        new("Read My Products", CustomActions.ReadMy, CustomResources.ProductTemplates),
        new("Read Products", CustomActions.Read, CustomResources.ProductTemplates), // For individual users to see all products
        new("Search Products", CustomActions.Search, CustomResources.ProductTemplates),

        new("Create My StoreProduct", CustomActions.CreateMy, CustomResources.Products),
        new("Read My StoreProduct", CustomActions.ReadMy, CustomResources.Products),
        new("Update My StoreProduct", CustomActions.UpdateMy, CustomResources.Products),
        new("Delete My StoreProduct", CustomActions.DeleteMy, CustomResources.Products),
        new("Search StoreProducts", CustomActions.Search, CustomResources.Products),
        new("Search My Stores StoreProducts", CustomActions.SearchMy, CustomResources.Products), // For store owners to ssearch their own store's products
    ];

    private static readonly HashSet<CustomPermission> _systemAdmin =
    [
        new("Manage Hangfire Dashboard", CustomActions.Manage, CustomResources.Hangfire),

        new("Create Users", CustomActions.Create, CustomResources.ApplicationUsers),
        new("Read Users", CustomActions.Read, CustomResources.ApplicationUsers),
        new("Update Users", CustomActions.Update, CustomResources.ApplicationUsers),
        new("Delete Users", CustomActions.Delete, CustomResources.ApplicationUsers),

        new("Create Stores", CustomActions.Create, CustomResources.Stores),
        new("Read Stores", CustomActions.Read, CustomResources.Stores),
        new("Update Stores", CustomActions.Update, CustomResources.Stores),
        new("Delete Stores", CustomActions.Delete, CustomResources.Stores),

        // In this marketplace system, the products those store owners can sell are managed by the system admins
        // You may think of this as Trendyol, Hepsiburada, and sellers can only sell the products that are predefined by the system admins (then customize prices and etc. see ->StoreProduct entity).
        new("Create Products", CustomActions.Create, CustomResources.ProductTemplates),
        new("Read Products", CustomActions.Read, CustomResources.ProductTemplates),
        new("Update Products", CustomActions.Update, CustomResources.ProductTemplates),
        new("Delete Products", CustomActions.Delete, CustomResources.ProductTemplates),
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
