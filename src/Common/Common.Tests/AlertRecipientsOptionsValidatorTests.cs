using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class AlertRecipientsOptionsValidatorTests
{
    private static AlertRecipientsOptions Valid() => new()
    {
        RoleName = "staff",
        ReconcileCron = "15 3 * * *",
        EmailSubjectTemplate = "[Critical alert] {0}",
        EmailHtmlBodyTemplate = "<h3>{0}</h3><p>{1}</p>",
        EmailTextBodyTemplate = "{0}\n\n{1}"
    };

    [Fact]
    public void ValidOptions_PassValidation()
    {
        var result = new AlertRecipientsOptionsValidator().Validate(Valid());

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(nameof(AlertRecipientsOptions.RoleName))]
    [InlineData(nameof(AlertRecipientsOptions.ReconcileCron))]
    [InlineData(nameof(AlertRecipientsOptions.EmailSubjectTemplate))]
    [InlineData(nameof(AlertRecipientsOptions.EmailHtmlBodyTemplate))]
    [InlineData(nameof(AlertRecipientsOptions.EmailTextBodyTemplate))]
    public void EmptyValue_IsInvalid(string property)
    {
        var options = Valid();
        typeof(AlertRecipientsOptions).GetProperty(property)!.SetValue(options, string.Empty);

        var result = new AlertRecipientsOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == property);
    }

    [Fact]
    public void SubjectTemplate_WithoutTheTitlePlaceholder_IsInvalid()
    {
        var options = Valid();
        options.EmailSubjectTemplate = "[Critical alert]";

        var result = new AlertRecipientsOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(AlertRecipientsOptions.EmailSubjectTemplate));
    }

    [Theory]
    [InlineData(nameof(AlertRecipientsOptions.EmailHtmlBodyTemplate))]
    [InlineData(nameof(AlertRecipientsOptions.EmailTextBodyTemplate))]
    public void BodyTemplate_WithoutTheMessagePlaceholder_IsInvalid(string property)
    {
        var options = Valid();
        typeof(AlertRecipientsOptions).GetProperty(property)!.SetValue(options, "{0} only");

        var result = new AlertRecipientsOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == property);
    }
}
