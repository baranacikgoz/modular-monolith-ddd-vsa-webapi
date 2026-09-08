using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Inventory;

public sealed record GetStockLevelRequest(DefaultIdType ProductId) : IInterModuleRequest<GetStockLevelResponse>;

public sealed record GetStockLevelResponse(int QuantityOnHand, int AvailableQuantity);
