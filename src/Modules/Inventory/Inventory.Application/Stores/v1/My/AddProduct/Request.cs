using Common.Application.ModelBinders;
using Inventory.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Application.Stores.v1.My.AddProduct;

internal sealed record Request(
    [ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId ProductId,
    int Quantity,
    decimal Price);
