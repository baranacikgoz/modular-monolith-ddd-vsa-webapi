namespace Products.Domain.Stores;

/// <summary>
///     The numeric values are stored: Active and Inactive keep the values rows had before the application flow existed.
///     A store created directly starts Active; one that applied starts PendingApproval and becomes Active on approval.
///     Inactive is final, and only a Suspended store can be reinstated.
/// </summary>
public enum StoreStatus
{
    Active = 0,
    Inactive = 1,
    PendingApproval = 2,
    Suspended = 3
}
