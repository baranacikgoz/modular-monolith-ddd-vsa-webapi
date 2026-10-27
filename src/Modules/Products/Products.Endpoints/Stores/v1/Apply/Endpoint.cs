using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Security;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.InterModuleRequests.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Infrastructure.RateLimiting;
using Products.Infrastructure.Telemetry;

namespace Products.Endpoints.Stores.v1.Apply;

/// <summary>
///     Anonymous store application. The applicant first proves they own the email address: the request carries the
///     verification token <c>POST /otp/email/verify</c> issued after the mailed code, checked through
///     <see cref="VerifyEmailOtpRequest" /> (Notifications' OTP store) under <see cref="EmailVerificationPurposes.NewAddress" />.
///     Then the owner's identity-provider account is created (<see cref="ProvisionUserRequest" />) and the store is saved
///     as PendingApproval; the two writes cannot share a transaction. When the save fails after the account exists, the account is deleted again
///     (<see cref="DeprovisionUserRequest" />), otherwise a retry would collide with the half-created account.
///     This is the compensation exception to the functional pipeline (CLAUDE.md section 3): the compensation after the
///     failed save is written as plain control flow in <see cref="SaveOrCompensateAsync" />, everything around it is a chain.
///     The password crosses the bus encrypted (<see cref="SecretProtector" />), never in clear.
///     No role is assigned and no token is returned; the store-owner role follows the approval through the outbox.
/// </summary>
internal static partial class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapPost("applications", ApplyAsync)
            .WithDescription("Apply for a store. Creates the owner's account and a store that waits for approval; no role is assigned and no token is returned.")
            .AllowAnonymous()
            .RequireRateLimiting(RateLimitingConstants.StoreApplication)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> ApplyAsync(
        Request request,
        IInterModuleRequestClient<VerifyEmailOtpRequest, VerifyEmailOtpResponse> verifyEmailClient,
        IInterModuleRequestClient<ProvisionUserRequest, ProvisionUserResponse> provisionClient,
        IInterModuleRequestClient<DeprovisionUserRequest, DeprovisionUserResponse> deprovisionClient,
        IProductsDbContext dbContext,
        SecretProtector secretProtector,
        ILogger<Request> logger,
        CancellationToken cancellationToken)
    {
        using var activity = ProductsTelemetry.ActivitySource.StartActivityForCaller();

        // The identity provider stores emails lowercased; the same form must reach it, the OTP store and the duplicate check.
#pragma warning disable CA1308
        var email = request.Email.Trim().ToLowerInvariant();
#pragma warning restore CA1308

        // Proof of mailbox ownership comes first: no account is created for an address nobody proved they own. The
        // token is single use, so a later failure (a weak password, a taken address) needs a fresh email code.
        var verified = (await verifyEmailClient.SendAsync(
            new VerifyEmailOtpRequest(email, request.EmailVerificationToken, EmailVerificationPurposes.NewAddress),
            cancellationToken)).ToResult();

        return await verified
            .BindAsync<ApplicationUserId>(async () => (await provisionClient.SendAsync(
                new ProvisionUserRequest(
                    email, request.PhoneNumber, secretProtector.Encrypt(request.Password), request.FirstName.Trim(), request.LastName.Trim()),
                cancellationToken)).ToResult())
            .BindAsync(ownerId => SaveOrCompensateAsync(
                Store.Apply(ownerId, request.StoreName, request.StoreDescription, request.StoreAddress),
                dbContext, deprovisionClient, logger, cancellationToken))
            .TapAsync(_ => ProductsTelemetry.StoreApplications.Add(1))
            .MapAsync(store => new Response { Id = store.Id })
            .TapActivityAsync(activity);
    }

    /// <summary>
    ///     A unique-constraint violation comes back as a failed result, anything else (a dropped connection, a timeout)
    ///     is thrown. Both delete the account first: the applicant retries the whole application either way, and a
    ///     surviving account would turn that retry into EmailAlreadyRegistered.
    /// </summary>
    internal static async Task<Result<Store>> SaveOrCompensateAsync(
        Store store,
        IProductsDbContext dbContext,
        IInterModuleRequestClient<DeprovisionUserRequest, DeprovisionUserResponse> deprovisionClient,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        dbContext.Stores.Add(store);

        Result saved;
        try
        {
            saved = await dbContext.SaveChangesDetectingUniqueViolationAsync(
                Error.ViolatesUniqueConstraint(nameof(Store)), cancellationToken);
        }
        catch (Exception)
        {
            await DeprovisionAsync(store.OwnerId, deprovisionClient, logger);
            throw;
        }

        if (saved.IsFailure)
        {
            await DeprovisionAsync(store.OwnerId, deprovisionClient, logger);
            return saved.Error!;
        }

        return store;
    }

    // CancellationToken.None: the request may already be cancelled, which is often why the save failed. A compensation
    // that fails too leaves an orphan account an operator must remove, so it is logged and never replaces the original failure.
    private static async Task DeprovisionAsync(
        ApplicationUserId ownerId,
        IInterModuleRequestClient<DeprovisionUserRequest, DeprovisionUserResponse> deprovisionClient,
        ILogger logger)
    {
        try
        {
            await deprovisionClient.SendAsync(new DeprovisionUserRequest(ownerId), CancellationToken.None);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            LogCompensationFailed(logger, ownerId, ex);
        }
    }

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Store application save failed and the owner account {OwnerId} could not be deleted; it must be removed by hand.")]
    private static partial void LogCompensationFailed(ILogger logger, ApplicationUserId ownerId, Exception exception);
}
