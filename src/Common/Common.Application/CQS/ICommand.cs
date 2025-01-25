using Common.Domain.ResultMonad;
using MediatR;

namespace Common.Application.CQS;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

public interface ICommand : IRequest<Result>
{
}
