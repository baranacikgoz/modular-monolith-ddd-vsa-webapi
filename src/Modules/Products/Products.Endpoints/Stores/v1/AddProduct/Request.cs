using Products.Domain.ProductTemplates;

namespace Products.Endpoints.Stores.v1.AddProduct;

public sealed record Request(ProductTemplateId ProductTemplateId, string Name, string Description, int Quantity, decimal Price);
