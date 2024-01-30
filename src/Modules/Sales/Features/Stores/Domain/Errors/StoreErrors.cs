
using System.Net;
using Common.Core.Contracts.Results;

namespace Sales.Features.Stores.Domain.Errors;

internal static class StoreErrors
{
    public static readonly Error ProductNotFound = new(nameof(ProductNotFound), HttpStatusCode.NotFound);
}
