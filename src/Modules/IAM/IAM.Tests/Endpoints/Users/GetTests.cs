using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class GetTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed record UserResponse(
        Guid Id, string Username, string? FirstName, string? LastName, string? Email, string? PhoneNumber,
        DateOnly? BirthDate, bool Enabled, DateTimeOffset CreatedOn);

    [Fact]
    public async Task GetUser_AsStaff_ReturnsUser()
    {
        var target = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var staff = await IamTestClient.LoginByEmailAsync(Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword);

        var user = await IamTestClient.Authorized(Factory, staff)
            .GetFromJsonAsync<UserResponse>(new Uri($"/users/{target.Subject}", UriKind.Relative));

        Assert.NotNull(user);
        Assert.Equal(target.Subject, user.Id.ToString());
        Assert.Equal(SeedUsers.BasicPhone, user.Username);
        Assert.Equal(SeedUsers.BasicFirstName, user.FirstName);
        Assert.True(user.Enabled);
    }

    [Fact]
    public async Task GetUser_AsBasicUser_Returns403()
    {
        var basic = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);

        using var response = await IamTestClient.Authorized(Factory, basic)
            .GetAsync(new Uri($"/users/{basic.Subject}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_UnknownId_Returns404()
    {
        var staff = await IamTestClient.LoginByEmailAsync(Factory, SeedUsers.StaffEmail, SeedUsers.StaffPassword);

        using var response = await IamTestClient.Authorized(Factory, staff)
            .GetAsync(new Uri($"/users/{Guid.NewGuid()}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
