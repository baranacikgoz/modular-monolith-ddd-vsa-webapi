using System.Globalization;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using FluentValidation;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Extensions;
using IdentityAndAuth.Features.Common.Validations;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.SelfRegister;

public sealed record Request(
        string PhoneVerificationToken,
        string PhoneNumber,
        string FirstName,
        string LastName,
        string NationalIdentityNumber,
        string BirthDate)
        : IRequest<Result<Response>>;
