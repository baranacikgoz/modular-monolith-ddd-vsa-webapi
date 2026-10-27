using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Inventory.Tests.Support;

/// <summary>
///     A throw-away HTTP/1.1 server on the loopback interface that answers every request with one canned response and counts
///     the connections it accepted. It exists to prove what a real socket does (which address was connected to, whether a
///     redirect was followed), which a fake message handler cannot show.
/// </summary>
internal sealed class LoopbackHttpServer : IDisposable
{
    private readonly TcpListener _listener = new(IPAddress.Loopback, 0);
    private readonly CancellationTokenSource _stop = new();
    private readonly Task _acceptLoop;
    private int _connections;

    public LoopbackHttpServer(string statusLine = "200 OK", string? location = null)
    {
        _listener.Start();
        _acceptLoop = Task.Run(() => AcceptLoopAsync(BuildResponse(statusLine, location)), CancellationToken.None);
    }

    public int Port => ((IPEndPoint)_listener.LocalEndpoint).Port;

    /// <summary>Connections accepted so far, whether or not a request followed.</summary>
    public int Connections => Volatile.Read(ref _connections);

    public void Dispose()
    {
        _stop.Cancel();
        _listener.Stop();
        try
        {
            _acceptLoop.Wait(TimeSpan.FromSeconds(5), CancellationToken.None);
        }
        catch (AggregateException)
        {
            // The loop ends with the listener; what it threw while stopping is of no interest.
        }

        _listener.Dispose();
        _stop.Dispose();
    }

    private static string BuildResponse(string statusLine, string? location)
    {
        var builder = new StringBuilder($"HTTP/1.1 {statusLine}\r\nContent-Length: 0\r\nConnection: close\r\n");
        if (location is not null)
        {
            builder.Append("Location: ").Append(location).Append("\r\n");
        }

        return builder.Append("\r\n").ToString();
    }

    private async Task AcceptLoopAsync(string response)
    {
        while (!_stop.IsCancellationRequested)
        {
            try
            {
                using var client = await _listener.AcceptTcpClientAsync(_stop.Token);
                Interlocked.Increment(ref _connections);
                await using var stream = client.GetStream();
                await ReadRequestAsync(stream, _stop.Token);
                await stream.WriteAsync(Encoding.ASCII.GetBytes(response), _stop.Token);
            }
            catch (Exception ex) when (ex is OperationCanceledException or ObjectDisposedException or IOException or SocketException)
            {
                if (_stop.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }

    /// <summary>Reads the head and, when the request has one, the body, so the client is never reset mid-write.</summary>
    private static async Task ReadRequestAsync(Stream stream, CancellationToken cancellationToken)
    {
        var received = new StringBuilder();
        var buffer = new byte[1024];
        var headEnd = -1;
        while (headEnd < 0)
        {
            var read = await stream.ReadAsync(buffer, cancellationToken);
            if (read == 0)
            {
                return;
            }

            received.Append(Encoding.ASCII.GetString(buffer, 0, read));
            headEnd = received.ToString().IndexOf("\r\n\r\n", StringComparison.Ordinal);
        }

        var head = received.ToString(0, headEnd);
        var lengthLine = head.Split("\r\n").FirstOrDefault(
            line => line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase));
        if (lengthLine is null || !int.TryParse(lengthLine["Content-Length:".Length..].Trim(), out var length))
        {
            return;
        }

        var missing = length - (received.Length - (headEnd + 4));
        while (missing > 0)
        {
            var read = await stream.ReadAsync(buffer, cancellationToken);
            if (read == 0)
            {
                return;
            }

            missing -= read;
        }
    }
}
