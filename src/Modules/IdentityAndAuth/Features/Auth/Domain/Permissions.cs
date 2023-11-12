using Common.Core.Auth;

namespace IdentityAndAuth.Features.Auth.Domain;

internal static class CustomPermissions
{
    private static readonly HashSet<CustomPermission> _basic = new()
    {
        new("Read My Profile", RfActions.ReadMy, RfResources.Users),
        new("Update My Profile", RfActions.UpdateMy, RfResources.Users),
        new("Create My Appointments", RfActions.CreateMy, RfResources.Appointments),
        new("Read My Appointments", RfActions.ReadMy, RfResources.Appointments),
        new("Update My Appointments", RfActions.UpdateMy, RfResources.Appointments),
        new("Read Venues", RfActions.Read, RfResources.Venues),
    };

    private static readonly HashSet<CustomPermission> _venueAdmin = new()
    {
        new("Create My Venue", RfActions.CreateMy, RfResources.Venues),
        new("Read My Venue", RfActions.ReadMy, RfResources.Venues),
        new("Update My Venue", RfActions.UpdateMy, RfResources.Venues),
        new("Read Appointments", RfActions.Read, RfResources.Appointments),
    };

    private static readonly HashSet<CustomPermission> _systemAdmin = new()
    {
        new("Create Users", RfActions.Create, RfResources.Users),
        new("Read Users", RfActions.Read, RfResources.Users),
        new("Update Users", RfActions.Update, RfResources.Users),
        new("Delete Users", RfActions.Delete, RfResources.Users),
        new("Create Venues", RfActions.Create, RfResources.Venues),
        new("Read Venues", RfActions.Read, RfResources.Venues),
        new("Update Venues", RfActions.Update, RfResources.Venues),
        new("Delete Venues", RfActions.Delete, RfResources.Venues),
        new("Create Appointments", RfActions.Create, RfResources.Appointments),
        new("Read Appointments", RfActions.Read, RfResources.Appointments),
        new("Update Appointments", RfActions.Update, RfResources.Appointments),
        new("Delete Appointments", RfActions.Delete, RfResources.Appointments),
    };

    public static readonly IReadOnlySet<string> Basic = _basic.Select(r => r.Name).ToHashSet();
    public static readonly IReadOnlySet<string> VenueAdmin = _venueAdmin.Select(r => r.Name).ToHashSet();
    public static readonly IReadOnlySet<string> SystemAdmin = _systemAdmin.Select(r => r.Name).ToHashSet();
}
