using Products.Domain.ProductTemplates;
using Xunit;

namespace Products.Tests.ProductTemplates;

public class ProductTemplateTests
{
    private const string Brand = "Sample Brand";
    private const string Model = "Sample Model";
    private const string Color = "Sample Color";

    [Fact]
    public void Create_WithValidParameters_ReturnsActiveTemplate()
    {
        // Act
        var template = ProductTemplate.Create(Brand, Model, Color);

        // Assert
        Assert.NotNull(template);
        Assert.True(template.IsActive);
        Assert.Equal(Brand, template.Brand);
        Assert.Equal(Model, template.Model);
        Assert.Equal(Color, template.Color);
    }

    [Fact]
    public void Activate_WhenInactive_SetsIsActiveToTrue()
    {
        // Arrange
        var template = ProductTemplate.Create(Brand, Model, Color);
        template.Deactivate();

        // Act
        var result = template.Activate();

        // Assert
        Assert.False(result.IsFailure);
        Assert.True(template.IsActive);
    }

    [Fact]
    public void Deactivate_WhenActive_SetsIsActiveToFalse()
    {
        // Arrange
        var template = ProductTemplate.Create(Brand, Model, Color);

        // Act
        var result = template.Deactivate();

        // Assert
        Assert.False(result.IsFailure);
        Assert.False(template.IsActive);
    }
}
