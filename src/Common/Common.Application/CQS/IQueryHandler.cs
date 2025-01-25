using Common.Domain.ResultMonad;
using MediatR;

namespace Common.Application.CQS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}
