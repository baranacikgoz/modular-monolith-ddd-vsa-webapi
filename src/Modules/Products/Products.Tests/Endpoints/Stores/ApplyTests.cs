using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Security;
using Common.InterModuleRequests.IAM;
using Common.InterModuleRequests.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;
using Products.Endpoints.Stores.v1.Apply;
using Products.Tests.Support;
using Xunit;
using Endpoint = Products.Endpoints.Stores.v1.Apply.Endpoint;

namespace Products.Tests.Endpoints.Stores;

public class ApplyTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly Uri Route = new("/v1/stores/applications", UriKind.Relative);

    private static Request ValidRequest() => new()
    {
        Email = "  Ada.Lovelace@Example.COM ",
        EmailVerificationToken = "verification-token",
        PhoneNumber = "905" + string.Concat(Guid.NewGuid().ToByteArray()[..9].Select(b => (char)('0' + (b % 10)))),
        Password = "A-Valid-Password-1",
        FirstName = "Ada",
        LastName = "Lovelace",
        StoreName = "Analytical Goods",
        StoreDescription = "Engines and parts",
        StoreAddress = "12 St James Square"
    };

    private static async Task<string?> ErrorKeyAsync(HttpResponseMessage response)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("errorKey").GetString();
    }

    private void ProvisioningReturns(ProvisionUserResponse response)
    {
        ProvisionUserClient.SendAsync(Arg.Any<ProvisionUserRequest>(), Arg.Any<CancellationToken>()).Returns(response);
    }

    [Fact]
    public async Task Apply_ValidRequest_ProvisionsTheOwnerAndSavesAPendingStore()
    {
        var ownerId = new ApplicationUserId(Guid.NewGuid());
        ProvisioningReturns(new ProvisionUserResponse(ownerId, ProvisionUserFailureReason.None));
        var request = ValidRequest();

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions);
        var store = await Db.Stores.AsNoTracking().SingleAsync(s => s.Id == body!.Id);
        Assert.Equal(StoreStatus.PendingApproval, store.Status);
        Assert.Equal(ownerId, store.OwnerId);
        Assert.Equal(request.StoreName, store.Name);
        await ProvisionUserClient.Received(1).SendAsync(
            Arg.Is<ProvisionUserRequest>(r => r.Email == "ada.lovelace@example.com"
                                               && r.PhoneNumber == request.PhoneNumber
                                               && r.FirstName == "Ada"
                                               && r.LastName == "Lovelace"),
            Arg.Any<CancellationToken>());
        await DeprovisionUserClient.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
        var audit = await Db.AuditLog.AsNoTracking().SingleAsync(e => e.AggregateId == store.Id.Value);
        Assert.IsType<V1StoreApplicationSubmittedDomainEvent>(audit.Event);
        Assert.Empty(await OutboxEventsAsync());
    }

    [Fact]
    public async Task Apply_ValidRequest_ChecksTheEmailVerificationTokenOfTheNormalizedAddress()
    {
        var request = ValidRequest();

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await VerifyEmailOtpClient.Received(1).SendAsync(
            Arg.Is<VerifyEmailOtpRequest>(r => r.Email == "ada.lovelace@example.com"
                                               && r.Otp == request.EmailVerificationToken
                                               && r.Purpose == EmailVerificationPurposes.NewAddress),
            Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData(OtpVerificationFailureReason.InvalidOtp, "InvalidEmailVerificationToken")]
    [InlineData(OtpVerificationFailureReason.TooManyAttempts, "TooManyEmailVerificationAttempts")]
    public async Task Apply_WhenTheEmailVerificationTokenIsRefused_ReturnsTheErrorOfThatReasonAndTouchesNothing(
        OtpVerificationFailureReason reason, string expectedKey)
    {
        VerifyEmailOtpClient.SendAsync(Arg.Any<VerifyEmailOtpRequest>(), Arg.Any<CancellationToken>())
            .Returns(new VerifyEmailOtpResponse(reason));

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, ValidRequest());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(expectedKey, await ErrorKeyAsync(response));
        await ProvisionUserClient.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
        Assert.Empty(await Db.Stores.AsNoTracking().ToListAsync());
    }

    [Fact]
    public async Task Apply_TokenIssuedForAnAddressThatAlreadyHasAUser_ReturnsEmailAlreadyRegisteredAndTouchesNothing()
    {
        VerifyEmailOtpClient.SendAsync(
                Arg.Is<VerifyEmailOtpRequest>(r => r.Purpose == EmailVerificationPurposes.NewAddress), Arg.Any<CancellationToken>())
            .Returns(new VerifyEmailOtpResponse(OtpVerificationFailureReason.InvalidOtp));
        VerifyEmailOtpClient.SendAsync(
                Arg.Is<VerifyEmailOtpRequest>(r => r.Purpose == EmailVerificationPurposes.ExistingAddress), Arg.Any<CancellationToken>())
            .Returns(new VerifyEmailOtpResponse(OtpVerificationFailureReason.None));
        var request = ValidRequest();

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, request);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("EmailAlreadyRegistered", await ErrorKeyAsync(response));
        await VerifyEmailOtpClient.Received(1).SendAsync(
            Arg.Is<VerifyEmailOtpRequest>(r => r.Email == "ada.lovelace@example.com"
                                               && r.Otp == request.EmailVerificationToken
                                               && r.Purpose == EmailVerificationPurposes.ExistingAddress),
            Arg.Any<CancellationToken>());
        await ProvisionUserClient.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
        Assert.Empty(await Db.Stores.AsNoTracking().ToListAsync());
    }

    [Fact]
    public async Task Apply_NewAddressTokenRunsOutOfAttempts_DoesNotTryTheExistingAddressPurpose()
    {
        VerifyEmailOtpClient.SendAsync(Arg.Any<VerifyEmailOtpRequest>(), Arg.Any<CancellationToken>())
            .Returns(new VerifyEmailOtpResponse(OtpVerificationFailureReason.TooManyAttempts));

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, ValidRequest());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("TooManyEmailVerificationAttempts", await ErrorKeyAsync(response));
        await VerifyEmailOtpClient.Received(1).SendAsync(Arg.Any<VerifyEmailOtpRequest>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Apply_TokenNeitherPurposeAccepts_ReturnsInvalidEmailVerificationTokenAfterTryingBoth()
    {
        VerifyEmailOtpClient.SendAsync(Arg.Any<VerifyEmailOtpRequest>(), Arg.Any<CancellationToken>())
            .Returns(new VerifyEmailOtpResponse(OtpVerificationFailureReason.InvalidOtp));

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, ValidRequest());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("InvalidEmailVerificationToken", await ErrorKeyAsync(response));
        await VerifyEmailOtpClient.Received(2).SendAsync(Arg.Any<VerifyEmailOtpRequest>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Apply_WithoutAnEmailVerificationToken_ReturnsBadRequestAndNeverChecksOrProvisions()
    {
        var request = ValidRequest() with { EmailVerificationToken = "" };

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        await VerifyEmailOtpClient.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
        await ProvisionUserClient.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
    }

    [Fact]
    public async Task Apply_ValidRequest_SendsThePasswordEncryptedAndNeverInClear()
    {
        ProvisioningReturns(new ProvisionUserResponse(new ApplicationUserId(Guid.NewGuid()), ProvisionUserFailureReason.None));
        var request = ValidRequest();

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var sent = ProvisionUserClient.ReceivedCalls().SelectMany(call => call.GetArguments()).OfType<ProvisionUserRequest>().Single();
        Assert.DoesNotContain(request.Password, JsonSerializer.Serialize(sent), StringComparison.Ordinal);
        Assert.DoesNotContain(request.Password, Encoding.UTF8.GetString(sent.EncryptedPassword), StringComparison.Ordinal);
        Assert.Equal(request.Password, Scope.ServiceProvider.GetRequiredService<SecretProtector>().Decrypt(sent.EncryptedPassword));
    }

    [Theory]
    [InlineData(ProvisionUserFailureReason.PhoneNumberAlreadyRegistered, HttpStatusCode.Conflict, "PhoneNumberAlreadyRegistered")]
    [InlineData(ProvisionUserFailureReason.EmailAlreadyRegistered, HttpStatusCode.Conflict, "EmailAlreadyRegistered")]
    [InlineData(ProvisionUserFailureReason.Rejected, HttpStatusCode.BadRequest, "IdentityProviderRejectedUser")]
    [InlineData(ProvisionUserFailureReason.IdentityProviderUnavailable, HttpStatusCode.BadGateway, "IdentityProviderUnavailable")]
    public async Task Apply_WhenProvisioningFails_ReturnsTheErrorOfThatReasonAndSavesNothing(
        ProvisionUserFailureReason reason, HttpStatusCode expectedStatus, string expectedKey)
    {
        ProvisioningReturns(new ProvisionUserResponse(UserId: null, reason, RejectionMessage: "Password too weak."));

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, ValidRequest());

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal(expectedKey, await ErrorKeyAsync(response));
        Assert.Empty(await Db.Stores.AsNoTracking().ToListAsync());
        await DeprovisionUserClient.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
    }

    [Fact]
    public async Task Apply_WhenTheSaveReturnsAConflict_DeletesTheJustCreatedOwner()
    {
        // The identity provider hands back an id that already owns a store, so the unique owner index rejects the save.
        var takenOwner = new ApplicationUserId(Guid.NewGuid());
        Db.Stores.Add(Store.Create(takenOwner, "Existing", "Description", "Address"));
        await Db.SaveChangesAsync();
        ProvisioningReturns(new ProvisionUserResponse(takenOwner, ProvisionUserFailureReason.None));

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, ValidRequest());

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("ViolatesUniqueConstraint", await ErrorKeyAsync(response));
        await DeprovisionUserClient.Received(1).SendAsync(
            Arg.Is<DeprovisionUserRequest>(r => r.UserId == takenOwner), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SaveOrCompensate_WhenTheSaveThrows_DeletesTheOwnerAndRethrows()
    {
        var store = Store.Apply(new ApplicationUserId(Guid.NewGuid()), "Name", "Description", "Address");
        using var failingDb = new ConflictingProductsDbContext(Db, () => throw new InvalidOperationException("database is gone"));

        var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            Endpoint.SaveOrCompensateAsync(store, failingDb, DeprovisionUserClient, NullLogger.Instance, CancellationToken.None));

        Assert.Equal("database is gone", thrown.Message);
        await DeprovisionUserClient.Received(1).SendAsync(
            Arg.Is<DeprovisionUserRequest>(r => r.UserId == store.OwnerId), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SaveOrCompensate_WhenTheCompensationFailsToo_StillRethrowsTheSaveFailure()
    {
        var store = Store.Apply(new ApplicationUserId(Guid.NewGuid()), "Name", "Description", "Address");
        using var failingDb = new ConflictingProductsDbContext(Db, () => throw new InvalidOperationException("database is gone"));
        DeprovisionUserClient.SendAsync(Arg.Any<DeprovisionUserRequest>(), Arg.Any<CancellationToken>())
            .Returns<Task<DeprovisionUserResponse>>(_ => throw new TimeoutException("identity provider is gone"));

        var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            Endpoint.SaveOrCompensateAsync(store, failingDb, DeprovisionUserClient, NullLogger.Instance, CancellationToken.None));

        Assert.Equal("database is gone", thrown.Message);
    }

    [Fact]
    public async Task Apply_WithAnInvalidEmail_ReturnsBadRequestAndNeverProvisions()
    {
        var request = ValidRequest() with { Email = "not-an-email" };

        using var response = await Factory.CreateClient().PostAsJsonAsync(Route, request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        await ProvisionUserClient.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
    }
}
