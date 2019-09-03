using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TeslaCanBusInspector.Common.Sockets
{
    public sealed class SocketCanReader : ISocketCanReader
    {
        private TcpClient _tcpClient;
        private Task _readTask;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public async Task ConnectAsync(string host, ushort port, CancellationToken cancellationToken = default)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));
            if (port == 0) throw new ArgumentOutOfRangeException(nameof(port));

            var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(host, port).ConfigureAwait(false);

            var networkStream = tcpClient.GetStream();
            _readTask = ReadStream(networkStream, _cts.Token);

            _tcpClient = tcpClient;
        }

        private async Task ReadStream(NetworkStream stream, CancellationToken cancellationToken = default)
        {
            try
            {
                await ReadStreamInternal(stream, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
            }
            catch (IOException)
            {
            }
        }

        private static async Task ReadStreamInternal(NetworkStream stream, CancellationToken cancellationToken)
        {
            var buffer = new byte[4096];
            while (!cancellationToken.IsCancellationRequested)
            {
                var read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                if (read == 0)
                {
                    return;
                }

                // TODO: parse CAN frames
            }
        }

        public void Dispose()
        {
            _cts.Dispose();
            _tcpClient?.Dispose();
            _readTask?.Wait();
        }
    }

    public interface ISocketCanReader : IDisposable
    {
        Task ConnectAsync(string host, ushort port, CancellationToken cancellationToken);
    }
}
