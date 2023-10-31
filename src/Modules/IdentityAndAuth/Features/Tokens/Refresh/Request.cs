using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Options;
using IdentityAndAuth;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Auth.Jwt;
using IdentityAndAuth.Features.Tokens.Errors;
using IdentityAndAuth.Features.Tokens.Services;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Features.Users.Services;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Tokens.Refresh;

public sealed record Request(string ExpiredAccessToken, string RefreshToken) : IRequest<Result<Response>>;
