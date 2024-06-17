using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Pagination;

public record PaginationResult<T>(ICollection<T> Data, int TotalCount, int PageNumber, int PageSize)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
    public int NextPageNumber => HasNext ? PageNumber + 1 : TotalPages;
    public int PreviousPageNumber => HasPrevious ? PageNumber - 1 : 1;
}
