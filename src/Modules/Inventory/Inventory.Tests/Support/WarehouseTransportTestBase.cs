namespace Inventory.Tests.Support;

/// <summary>A fake warehouse transport and an <see cref="HttpClient" /> over it, disposed with the test class.</summary>
public abstract class WarehouseTransportTestBase : IDisposable
{
    protected WarehouseTransportTestBase()
    {
        Client = new HttpClient(Handler, disposeHandler: false);
    }

    protected FakeWarehouseHandler Handler { get; } = new();

    protected HttpClient Client { get; }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Client.Dispose();
            Handler.Dispose();
        }
    }
}
