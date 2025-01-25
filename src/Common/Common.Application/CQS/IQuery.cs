using Common.Domain.ResultMonad;
using MediatR;

namespace Common.Application.CQS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
