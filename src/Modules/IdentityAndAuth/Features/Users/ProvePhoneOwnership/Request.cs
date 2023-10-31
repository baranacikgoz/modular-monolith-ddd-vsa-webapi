using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using FluentValidation;
using IdentityAndAuth;
using IdentityAndAuth.Features.Common.Validations;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
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

namespace IdentityAndAuth.Features.Users.ProvePhoneOwnership;

public sealed record Request(string PhoneNumber, string Otp) : IRequest<Result<Response>>;
