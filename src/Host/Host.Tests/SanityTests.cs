namespace Host.Tests;

public class SanityTests
{
    [Fact]
    public async Task Boot_WithAllModules_ShouldResolveDependencies()
    {
        // Arrange
        await using var factory = new HostTestFactory();
        await factory.InitializeAsync();

        // Act & Assert — creating a client triggers the full server boot and DI graph resolution.
        var exception = Record.Exception(() => _ = factory.CreateClient());
        Assert.Null(exception);
    }
}
