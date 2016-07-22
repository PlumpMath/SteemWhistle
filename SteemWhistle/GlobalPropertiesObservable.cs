using System;
using WebSocketSharp;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Reactive.Subjects;

namespace SteemWhistle
{
    public static class GlobalPropertiesObservable
    {
        public static CancellationToken cancellationToken = new CancellationToken(false);
        public static IObservable<GlobalProperties> Create(string webSocketAddress)
        {
            return Observable.Create<GlobalProperties>((o) =>
                {
                    Int64 head_block_number = 0;
                    Int64 last_block_number = -1;
                    var ws = new WebSocket(webSocketAddress,
                        onOpen: () => Task.Run(() => Console.WriteLine("GlobalProperties Observable Connected.")),
                        onMessage: (e) => Task.Run(() =>
                        {
                            var obj = JsonConvert.DeserializeObject<GetDynamicGlobalPropertiesResult>(e.Text.ReadToEnd());

                            if (obj.result != null)
                            {
                                var globalProperties = obj.result;
                                head_block_number = globalProperties.head_block_number;

                                if (head_block_number != last_block_number)
                                {
                                    last_block_number = head_block_number;
                                    o.OnNext(globalProperties);
                                }
                            }
                        }),
                        onError: (e) =>
                        {
                            o.OnError(e.Exception);
                            return Task.Run(() => Console.WriteLine("Error: {0}", e.Message));
                        });

                    ws.Connect().Wait();

                    return Task.Run(() =>
                    {
                        while (cancellationToken.IsCancellationRequested == false)
                        {
                            ws.Send(@"{""jsonrpc"": ""2.0"",
                                        ""id"": 1
                                        ""method"": ""get_dynamic_global_properties"",
                                        ""params"": [],}");
                            Thread.Sleep(3000);
                        }
                        ws.Dispose();
                    });
                });
        }
    }
}