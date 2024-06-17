using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.v1.Search;
public sealed record Response(StoreId Id, string Name, string Description, Uri? LogoUrl, int ProductCount);
