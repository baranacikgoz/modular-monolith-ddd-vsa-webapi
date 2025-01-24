using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.ResultMonad;
using MediatR;

namespace Common.Application.CQS;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

public interface ICommand : IRequest<Result>
{
}
