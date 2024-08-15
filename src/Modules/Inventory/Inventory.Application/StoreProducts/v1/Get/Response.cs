using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain.StoreProducts;

namespace Inventory.Application.StoreProducts.v1.Get;
public sealed record Response(StoreProductId Id, string Name, string Description);
