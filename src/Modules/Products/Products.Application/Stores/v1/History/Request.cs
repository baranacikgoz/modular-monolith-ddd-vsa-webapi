using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Localization;
using Common.Application.Pagination;
using Microsoft.Extensions.Localization;

namespace Products.Application.Stores.v1.History;

public sealed record Request(int PageNumber, int PageSize) : PaginationRequest(PageNumber, PageSize);

public class RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : PaginationRequestValidator<Request>(localizer)
{
}
