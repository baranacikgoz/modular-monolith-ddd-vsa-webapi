using Common.Application.Auth;
using Common.Infrastructure.FeatureManagement;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515
#pragma warning disable CA1707
public sealed class HttpContextTargetingContextAccessorTests
{
    [Fact]
    public async Task GetContextAsync_WithCurrentUser_ReturnsUserIdAndGroups()
    {
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.IdAsString.Returns("user-123");
        currentUser.Roles.Returns(["Admin", "Beta"]);

        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(ICurrentUser)).Returns(currentUser);

        var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(httpContext);

        var sut = new HttpContextTargetingContextAccessor(accessor);

        var context = await sut.GetContextAsync();

        Assert.Equal("user-123", context.UserId);
        Assert.Equal(["Admin", "Beta"], context.Groups);
    }

    [Fact]
    public async Task GetContextAsync_WithoutCurrentUser_ReturnsNullUserIdAndEmptyGroups()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(ICurrentUser)).Returns(null);

        var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(httpContext);

        var sut = new HttpContextTargetingContextAccessor(accessor);

        var context = await sut.GetContextAsync();

        Assert.Null(context.UserId);
        Assert.Empty(context.Groups);
    }

    [Fact]
    public async Task GetContextAsync_WithNullHttpContext_ReturnsNullUserIdAndEmptyGroups()
    {
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns((HttpContext?)null);

        var sut = new HttpContextTargetingContextAccessor(accessor);

        var context = await sut.GetContextAsync();

        Assert.Null(context.UserId);
        Assert.Empty(context.Groups);
    }
}
#pragma warning restore CA1707
