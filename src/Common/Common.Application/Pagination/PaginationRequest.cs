using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Pagination;

public record PaginationRequest(int PageNumber, int PageSize)
{
    public int Skip => (PageNumber - 1) * PageSize;
}
