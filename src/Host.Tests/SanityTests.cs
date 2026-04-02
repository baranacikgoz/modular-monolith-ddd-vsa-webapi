namespace Host.Tests;

public class SanityTests : IAsyncLifetime
{
    [Fact]
    public async Task Boot_WithAllModules_ShouldResolveDependencies()
    {
        // Arrange
        var factory = new HostTestFactory();
        await factory.InitializeAsync();

        try
        {
            // Act & Assert
            // Creating a client triggers the server boot and DI graph resolution.
            var exception = await Record.ExceptionAsync(async () =>
            {
                _ = factory.CreateClient();
                await Task.CompletedTask;
            });

            Assert.Null(exception);
        }
        finally
        {
            await factory.DisposeAsync();
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}
