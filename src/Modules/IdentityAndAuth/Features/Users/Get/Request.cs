using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using IdentityAndAuth;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.Get;

internal sealed record Request(Guid Id) : IRequest<Result<Response>>;
