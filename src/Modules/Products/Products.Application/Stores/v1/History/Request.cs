using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Pagination;

namespace Products.Application.Stores.v1.History;

public sealed record Request(int PageNumber, int PageSize) : PaginationRequest(PageNumber, PageSize);
