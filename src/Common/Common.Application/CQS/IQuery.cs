using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.ResultMonad;
using MediatR;

namespace Common.Application.CQS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
