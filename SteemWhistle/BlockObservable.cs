using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace SteemWhistle
{
    public static class BlockObservable
    {
        public static CancellationToken cancellationToken = new CancellationToken(false);
        public static IObservable<Block> Create(string websocketAddress, IObservable<GlobalProperties> globalPropertiesObservable = null)
        {
            if (globalPropertiesObservable == null)
                globalPropertiesObservable = GlobalPropertiesObservable.Create(websocketAddress);

            return Observable.Create<Block>((o) =>
                {
                    var ws = new WebSocket(websocketAddress,
                    onOpen: () => Task.Run(() => Console.WriteLine("Block Observable Connected.")),
                    onMessage: (e) => Task.Run(() =>
                    {
                        var obj = JsonConvert.DeserializeObject<GetBlockResult>(e.Text.ReadToEnd());
                        var block = obj.result;
                        if (obj.result != null)
                        {
                            o.OnNext(block);
                        }
                    }
                    ),
                    onError: (e) =>
                    {
                        o.OnError(e.Exception);
                        return Task.Run(() => Console.WriteLine("Error: {0}", e.Message));
                    });

                    ws.Connect().Wait();
                    globalPropertiesObservable.Subscribe((gpo) =>
                    {
                        ws.Send(@"{""jsonrpc"": ""2.0"",
                                ""id"": 1
                                ""method"": ""get_block"",
                                ""params"": [" + gpo.head_block_number + "]}");
                    });

                    return Disposable.Create(() => ws.Dispose());
                });
        }
    }
}
