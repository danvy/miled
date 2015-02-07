using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiLED
{
    public class LEDAdminClient : LEDClient
    {
        public const string DefaultInHost = "0.0.0.0";
        public const string DefaultInService = "48899";
        public LEDAdminClient(string outHost = DefaultOutHost, string outService = DefaultOutService,
            string inHost = DefaultInHost, string inService = DefaultInService) : base(outHost, outService)
        {
            _admin = true;
            _inHost = inHost;
            _inService = inService;
        }
        public async Task<string> ReceiveDataAsync()
        {
#if NETFX_CORE || WINDOWS_PHONE
#else
            for (var i = 0; i < 3; i++)
            {
                if (_client.Available > 0)
                {
                    _receiveBuffer.AppendLine(Encoding.UTF8.GetString(_client.Receive(ref _inIP)));
                    break;
                }
                await DelayAsync(500);
            }
#endif
            var s = _receiveBuffer.ToString();
            _receiveBuffer.Clear();
            await Task.Run(() => { });
            return s;
        }
    }
}
