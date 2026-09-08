using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Products;

public sealed record GetProductRequest(DefaultIdType ProductId) : IInterModuleRequest<GetProductResponse>;

public sealed record GetProductResponse(string Name, bool StoreIsActive);
