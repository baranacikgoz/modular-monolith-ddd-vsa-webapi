using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Localization;
using Common.Application.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Products.Application.Stores.v1.History;

public class Request : PaginationRequest
{
}

public class RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : PaginationRequestValidator<Request>(localizer)
{
}
