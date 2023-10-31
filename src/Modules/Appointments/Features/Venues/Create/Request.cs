using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace Appointments.Features.Venues.Create;

public sealed record Request(string Name) : IRequest<Result<Response>>;
