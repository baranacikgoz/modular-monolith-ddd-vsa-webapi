namespace Host.Tests;

public class SanityTests
{
    [Fact]
    public void Boot_WithAllModules_ShouldResolveDependencies()
    {
        // Arrange
        using var factory = new HostTestFactory();

        // Act & Assert
        // Creating a client triggers the server boot and DI graph resolution.
        var exception = Record.Exception(() => _ = factory.CreateClient());

        Assert.Null(exception);
    }
}
