using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.DTOs;
using Products.Domain.Products;

namespace Products.Application.Products.DTOs;

public record ProductDto : AuditableEntityDto<ProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
