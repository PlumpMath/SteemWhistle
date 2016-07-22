using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace SteemWhistle
{
    public class Wallet : IDisposable
    {
        private WebSocket walletSocket;
        public Wallet(string webSocketAddress)
        {
            walletSocket = new WebSocket(webSocketAddress,
                    onOpen: () => Task.Run(() => Trace.WriteLine("Wallet socket connected.")),
                    onMessage: (e) => Task.Run(() =>
                    {
                        Trace.WriteLine(e.Text?.ReadToEnd());
                    }),
                    onError: (e) =>
                    {
                        return Task.Run(() => Trace.WriteLine("Error: {0}", e.Message));
                    });
        }

        public async Task<bool> Connect()
        {
            return await walletSocket.Connect();
        }

        public async Task<bool> Upvote(string voter, Comment c)
        {
            return await walletSocket.Send($"{{\"jsonrpc\": \"2.0\", \"method\": \"vote\", \"params\": [\"{voter}\", \"{c.author}\", \"{c.permlink}\", 100, true], \"id\": 1}}");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    walletSocket.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
