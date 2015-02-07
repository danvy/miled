using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiLED.ConsoleApp
{
    class Program
    {
        private static CancellationTokenSource cts = new CancellationTokenSource();
        static void Main(string[] args)
        {
            Task.Run(async () => await MainAsync(args)).Wait();
            Console.ReadKey();
        }
        static async Task MainAsync(string[] args)
        {
            var bridges = await LEDBridge.FindBridgesAsync();
            if (bridges.Count() > 0)
            {
                foreach (var bridge in bridges)
                {
                    Console.WriteLine(string.Format("Key={0}, Value={1}", bridge.Key, bridge.Value));
                }
                var bridgeIP = bridges.First().Value;
                var hotspots = await LEDBridge.ListHostspotsAsync(bridgeIP);
                foreach (var hotspot in hotspots)
                {
                    Console.WriteLine(string.Format("SSID={0}, Signal={1}%", hotspot.SSID, hotspot.Signal));
                }
                Console.WriteLine(await LEDBridge.VersionAsync(bridgeIP));
                if (await LEDBridge.SetupHotspotAsync(bridgeIP, "Dnet", "test"))
                {
                    Console.WriteLine("Setup ok");
                }
                else
                {
                    Console.WriteLine("Setup error");
                }

            }
        }
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            cts.Cancel();
        }
    }
}
