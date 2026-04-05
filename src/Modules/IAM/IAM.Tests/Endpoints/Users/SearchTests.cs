using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Application.Pagination;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Response = IAM.Endpoints.Users.VersionNeutral.Search.Response;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class SearchTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SearchTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private ApplicationUser CreateUser(string name, string lastName)
        => ApplicationUser.Create(
            name,
            lastName,
            "555" + _faker.Random.Number(1000000, 9999999).ToString(CultureInfo.InvariantCulture),
            _faker.Random.Long(10000000000L, 99999999999L).ToString(CultureInfo.InvariantCulture),
            DateOnly.FromDateTime(_faker.Date.Past(30)));

    [Fact]
    public async Task Search_WithSearchTerm_ReturnsMatchingUsers()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();

        var targetUser = CreateUser("Johnathan", "Silverstone");
        var otherUser = CreateUser("Maria", "Rodriguez");

        db.Users.Add(targetUser);
        db.Users.Add(otherUser);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri("/users/search?PageNumber=1&PageSize=10&searchTerm=Johnathan", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.True(result.TotalCount >= 1);
        Assert.Contains(result.Data, u => u.Name == "Johnathan" && u.LastName == "Silverstone");
    }

    [Fact]
    public async Task Search_WithNameFilter_ReturnsFilteredUsers()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();

        var targetName = "UniqueFirstName_" + Guid.NewGuid().ToString("N")[..8];
        var targetUser = CreateUser(targetName, "Jones");
        var otherUser = CreateUser("DifferentName", "Smith");

        db.Users.Add(targetUser);
        db.Users.Add(otherUser);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/users/search?PageNumber=1&PageSize=10&name={targetName}", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(targetName, result.Data.First().Name);
    }

    [Fact]
    public async Task Search_WithLastNameFilter_ReturnsFilteredUsers()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();

        var targetLastName = "UniqueLastName_" + Guid.NewGuid().ToString("N")[..8];
        var targetUser = CreateUser("Alice", targetLastName);
        var otherUser = CreateUser("Bob", "DifferentLastName");

        db.Users.Add(targetUser);
        db.Users.Add(otherUser);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/users/search?PageNumber=1&PageSize=10&lastName={targetLastName}", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(targetLastName, result.Data.First().LastName);
    }

    [Fact]
    public async Task Search_WithSearchTermExceedingMaxLength_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        var longSearchTerm = new string('a', Constants.SearchTermMaxLength + 1);

        // Act
        var response = await client.GetAsync(new Uri($"/users/search?PageNumber=1&PageSize=10&searchTerm={longSearchTerm}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
