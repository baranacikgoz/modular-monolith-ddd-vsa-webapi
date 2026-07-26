using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class ApiKeysOptionsValidatorTests
{
    private const string ValidHash = "62af8704764faf8ea82fc61ce9c4c3908b6cb97d463a634e9e587d7c885db0ef";

    private static ApiKeyEntry ValidEntry(
        string name = "github-actions-release",
        string keyHash = ValidHash,
        string[]? permissions = null) => new()
    {
        Name = name,
        KeyHash = keyHash,
        Permissions = permissions ?? ["Permissions.AppReleaseGates.Manage"]
    };

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var validator = new ApiKeysOptionsValidator();
        var result = validator.Validate(new ApiKeysOptions { Keys = [ValidEntry()] });

        Assert.True(result.IsValid);
    }

    [Fact]
    public void NoKeys_PassesValidation()
    {
        var validator = new ApiKeysOptionsValidator();
        var result = validator.Validate(new ApiKeysOptions { Keys = [] });

        Assert.True(result.IsValid);
    }

    [Fact]
    public void NullKeys_PassesValidation()
    {
        // The config binder resolves an empty JSON array to null rather than an empty list —
        // this is the realistic shape for an environment with no keys configured yet.
        var validator = new ApiKeysOptionsValidator();
        var result = validator.Validate(new ApiKeysOptions { Keys = null });

        Assert.True(result.IsValid);
    }

    [Fact]
    public void DuplicateNames_Fails()
    {
        var validator = new ApiKeysOptionsValidator();
        var result = validator.Validate(new ApiKeysOptions { Keys = [ValidEntry("dup"), ValidEntry("dup")] });

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmptyName_Fails()
    {
        var entry = ValidEntry(name: "");
        var validator = new ApiKeyEntryValidator();
        var result = validator.Validate(entry);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ApiKeyEntry.Name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-hex")]
    [InlineData("0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF0123456789ABCD")] // uppercase rejected
    [InlineData("0123456789abcdef")] // too short
    public void MalformedKeyHash_Fails(string hash)
    {
        var entry = ValidEntry(keyHash: hash);
        var validator = new ApiKeyEntryValidator();
        var result = validator.Validate(entry);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ApiKeyEntry.KeyHash));
    }

    [Fact]
    public void EmptyPermissions_Fails()
    {
        var entry = ValidEntry(permissions: []);
        var validator = new ApiKeyEntryValidator();
        var result = validator.Validate(entry);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ApiKeyEntry.Permissions));
    }

    [Theory]
    [InlineData("Permissions.Businesses.ReadMy")]
    [InlineData("Permissions.Businesses.UpdateMy")]
    [InlineData("Permissions.Businesses.CreateMy")]
    public void MyScopedPermission_Fails(string permission)
    {
        var entry = ValidEntry(permissions: [permission]);
        var validator = new ApiKeyEntryValidator();
        var result = validator.Validate(entry);

        Assert.False(result.IsValid);
    }
}
