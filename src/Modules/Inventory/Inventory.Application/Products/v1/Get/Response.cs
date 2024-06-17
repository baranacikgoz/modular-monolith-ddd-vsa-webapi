using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain.Products;

namespace Inventory.Application.Products.v1.Get;
public sealed record Response(ProductId Id, string Name, string Description);
