// Coded by Alex Danvy
// http://danvy.tv
// http://twitter.com/danvy
// http://github.com/danvy
// Licence Apache 2.0
// Use at your own risk, have fun

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
#if NETFX_CORE || WINDOWS_PHONE
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Networking;
#else
using System.Net.Sockets;
#endif

namespace MiLED
{
    public class LEDBridge
    {
        private static readonly object _lock = new object();
        private string _hostName = string.Empty;
        private string _serviceName = string.Empty;
        //10.10.100.254, ACCF2322ACDB,
        private static Regex _IPIdRegEx = new Regex(@"^(?<ip>\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b),.*(?<id>[0-9a-zA-Z]{12}).*$");
        //6,Dnet,00:19:70:31:0c:01,WPA1PSKWPA2PSK/TKIPAES,42,11b/g/n,NONE,In,NO,
        private static Regex _hotspotRegEx = new Regex(@"^(?<ch>\d{1,2}),(?<ssid>.*),(?<bssid>.*),(?<security>.*),(?<signal>\d{1,3}),(?<extch>.*),(?<nt>.*),(?<wps>.*),(?<dpid>.*),$");
        //+ok=V1.0.03a-JCY-7\r\n\r\n\r\n
        private static Regex _versionRegEx = new Regex(@"^(?<ok>[+ok=]{4})(?<ver>[0-9a-zA-Z\.\-]*).*$");
        private LEDClient _client = null;
        public LEDBridge()
        {
        }
        public LEDBridge(string hostName, string serviceName)
        {
            _hostName = hostName;
            _serviceName = serviceName;
        }
        private async Task DelayAsync(int millisecondsTimeout = 101)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(millisecondsTimeout));
        }
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
        public async Task SendCommandAsync(byte[] buffer)
        {
            if (_client == null)
            {
                lock (_lock)
                {
                    if (_client == null)
                        _client = new LEDClient(_hostName, _serviceName);
                }
            }
            await _client.SendDataAsync(buffer, buffer.Length);
        }
        public async Task StrobeMode(CancellationToken? ct = null)
        {
            while ((ct == null) || ((ct != null) && (ct.Value.CanBeCanceled) && (ct.Value.IsCancellationRequested)))
            {
                await SendCommandAsync(LEDCommands.AllOff);
                await DelayAsync();
                await SendCommandAsync(LEDCommands.AllOn);
                await DelayAsync();
            }
        }
        public async Task FadeDownAsync(CancellationToken? ct = null)
        {
            await SendCommandAsync(LEDCommands.Group1On);
            await DelayAsync();
            for (int i = 1; i < 10; i++)
            {
                if ((ct != null) && (ct.Value.CanBeCanceled) && (ct.Value.IsCancellationRequested))
                    break;
                await SendCommandAsync(LEDCommands.BrightnessDown);
                await DelayAsync(1000);
            }
        }
        public async Task FadeUpAsync(CancellationToken? ct = null)
        {
            await SendCommandAsync(LEDCommands.Group1On);
            await DelayAsync();
            for (int i = 1; i < 10; i++)
            {
                if ((ct != null) && (ct.Value.CanBeCanceled) && (ct.Value.IsCancellationRequested))
                    break;
                await SendCommandAsync(LEDCommands.BrightnessUp);
                await DelayAsync(1000);
            }
        }
        public async Task AllOffAsync()
        {
            await SendCommandAsync(LEDCommands.AllOff);
        }
        public async Task AllOnAsync()
        {
            await SendCommandAsync(LEDCommands.AllOn);
        }
        public async Task AllNightModeAsync()
        {
            await SendCommandAsync(LEDCommands.AllOff);
            await DelayAsync();
            await SendCommandAsync(LEDCommands.AllNight);
        }
        public async Task BrightenAsync()
        {
            await SendCommandAsync(LEDCommands.BrightnessUp);
        }
        public async Task DimAsync()
        {
            await SendCommandAsync(LEDCommands.BrightnessDown);
        }
        public async Task Group4NightModeAsync()
        {
            await SendCommandAsync(LEDCommands.Group4Off);
            await DelayAsync();
            await SendCommandAsync(LEDCommands.Group4Night);
        }
        public async Task Group3NightModeAsync()
        {
            await SendCommandAsync(LEDCommands.Group3Off);
            await DelayAsync();
            await SendCommandAsync(LEDCommands.Group3Night);
        }
        public async Task Group2NightModeAsync()
        {
            await SendCommandAsync(LEDCommands.Group2Off);
            await DelayAsync();
            await SendCommandAsync(LEDCommands.Group2Night);
        }
        public async Task Group1NightModeAsync()
        {
            await SendCommandAsync(LEDCommands.Group1Off);
            await DelayAsync();
            await SendCommandAsync(LEDCommands.Group1Night);
        }
        public async Task Group1OffAsync()
        {
            await SendCommandAsync(LEDCommands.Group1Off);
        }
        public async Task Group2OffAsync()
        {
            await SendCommandAsync(LEDCommands.Group2Off);
        }
        public async Task Group3OffAsync()
        {
            await SendCommandAsync(LEDCommands.Group3Off);
        }
        public async Task Group4OffAsync()
        {
            await SendCommandAsync(LEDCommands.Group4Off);
        }
        public async Task WakeUpCallAsync(CancellationToken? ct = null)
        {
            await AllOnAsync();
            await DelayAsync();
            for (int i = 0; i < 10; i++)
            {
                if ((ct != null) && (ct.Value.CanBeCanceled) && (ct.Value.IsCancellationRequested))
                    break;
                await DimAsync();
                await DelayAsync();
            }
            await RGBOnAsync();
            await DelayAsync();
            await RGBPrevModeAsync();
            await DelayAsync();
            for (int i = 0; i < 10; i++)
            {
                if ((ct != null) && (ct.Value.CanBeCanceled) && (ct.Value.IsCancellationRequested))
                    break;
                await RGBDimAsync();
                await DelayAsync();
            }
            for (int i = 0; i < 10; i++)
            {
                if ((ct != null) && (ct.Value.CanBeCanceled) && (ct.Value.IsCancellationRequested))
                    break;
                await DelayAsync(60000);
                await AllOnAsync();
                await DelayAsync();
                await BrightenAsync();
                await DelayAsync();
                await RGBOnAsync();
                await DelayAsync();
                await RGBPrevModeAsync();
                await DelayAsync();
                await RGBBrightenAsync();
            }
        }
        public async Task RGBOnAsync()
        {
            await SendCommandAsync(LEDCommands.RGBOn);
        }
        public async Task RGBOffAsync()
        {
            await SendCommandAsync(LEDCommands.RGBOff);
        }
        public async Task RGBColourAsync(byte color)
        {
            var cmd = (byte[])LEDCommands.RGBColour.Clone();
            cmd[1] = (byte)color;
            await SendCommandAsync(cmd);
        }
        public async Task RGBNextModeAsync()
        {
            await SendCommandAsync(LEDCommands.RGBDiscoNext);
        }
        public async Task RGBPrevModeAsync()
        {
            await SendCommandAsync(LEDCommands.RGBDiscoLast);
        }
        public async Task RGBBrightenAsync()
        {
            await SendCommandAsync(LEDCommands.RGBBrightnessUp);
        }
        public async Task RGBDimAsync()
        {
            await SendCommandAsync(LEDCommands.RGBBrightnessDown);
        }
        public async Task RGBSpeedUpAsync()
        {
            await SendCommandAsync(LEDCommands.RGBSpeedUp);
        }
        public async Task RGBSpeedDownAsync()
        {
            await SendCommandAsync(LEDCommands.RGBSpeedDown);
        }
        public async Task RGBWOff(LEDGroups group)
        {
            byte[] cmd;
            switch (group)
            {
                case LEDGroups.One:
                    cmd = LEDCommands.RGBWGroup1AllOff;
                    break;
                case LEDGroups.Two:
                    cmd = LEDCommands.RGBWGroup1AllOff;
                    break;
                case LEDGroups.Three:
                    cmd = LEDCommands.RGBWGroup1AllOff;
                    break;
                case LEDGroups.Four:
                    cmd = LEDCommands.RGBWGroup1AllOff;
                    break;
                default: //all
                    cmd = LEDCommands.RGBWOff;
                    break;
            }
            await SendCommandAsync(cmd);
        }
        public async Task RGBWOn(LEDGroups group)
        {
            byte[] cmd;
            switch (group)
            {
                case LEDGroups.One:
                    cmd = LEDCommands.RGBWGroup1AllOn;
                    break;
                case LEDGroups.Two:
                    cmd = LEDCommands.RGBWGroup1AllOn;
                    break;
                case LEDGroups.Three:
                    cmd = LEDCommands.RGBWGroup1AllOn;
                    break;
                case LEDGroups.Four:
                    cmd = LEDCommands.RGBWGroup1AllOn;
                    break;
                default: //all
                    cmd = LEDCommands.RGBWOn;
                    break;
            }
            await SendCommandAsync(cmd);
        }
        public async Task RGBWDiscoMode()
        {
            await SendCommandAsync(LEDCommands.RGBWDiscoMode);
        }
        public async Task RGBWDiscoSpeedSlower()
        {
            await SendCommandAsync(LEDCommands.RGBWDiscoSpeedSlower);
        }
        public async Task RGBWDiscoSpeedFaster()
        {
            await SendCommandAsync(LEDCommands.RGBWDiscoSpeedFaster);
        }
        public async Task RGBWWhiteAsync(LEDGroups group)
        {
            byte[] cmd;
            switch (group)
            {
                case LEDGroups.One:
                    cmd = LEDCommands.SetColorToWhiteGroup1;
                    break;
                case LEDGroups.Two:
                    cmd = LEDCommands.SetColorToWhiteGroup2;
                    break;
                case LEDGroups.Three:
                    cmd = LEDCommands.SetColorToWhiteGroup3;
                    break;
                case LEDGroups.Four:
                    cmd = LEDCommands.SetColorToWhiteGroup4;
                    break;
                default: //all
                    cmd = LEDCommands.SetColorToWhite;
                    break;
            }
            await RGBWOn(group);
            await DelayAsync();
            await SendCommandAsync(cmd);
        }
        public async Task RGBWBrightnessAsync(byte level)
        {
            if (level < 0x02)
            {
                level = 0x02;
            }
            else if (level > 0x27)
            {
                level = 0x27;
            }

            var cmd = (byte[])LEDCommands.RGBWBrightness.Clone();
            cmd[1] = level;
            await SendCommandAsync(cmd);
        }
        public async Task RGBWColorAsync(byte color)
        {
            var cmd = (byte[])LEDCommands.RGBWColor.Clone();
            cmd[1] = color;
            await SendCommandAsync(cmd);
        }
        public static async Task<Dictionary<string, string>> FindBridgesAsync()
        {
            var bridges = new Dictionary<string, string>();
            var client = new LEDAdminClient("10.10.100.255");
            try
            {
                for (var i = 0; i < 10; i++)
                {
                    await client.SendDataAsync("Link_Wi-Fi");
                }
                var data = string.Empty;
                while (!string.IsNullOrEmpty(data = await client.ReceiveDataAsync()))
                {
                    var ma = _IPIdRegEx.Match(data);
                    if (ma.Success)
                    {
                        var id = ma.Groups["id"].Value;
                        if (!bridges.ContainsKey(id))
                            bridges.Add(id, ma.Groups["ip"].Value);
                    }
                }
            }
            finally
            {
                client.Close();
            }
            return bridges;
        }
        public static async Task<List<LEDHotspot>> ListHostspotsAsync(string bridgeIP)
        {
            var hostspots = new List<LEDHotspot>();
            var client = new LEDAdminClient();
            try
            {
                if (await HandshakeAsync(client))
                {
                    await client.SendDataAsync("AT+WSCAN\r\n");
                    await Task.Delay(1000);
                    var data = string.Empty;
                    while (!string.IsNullOrEmpty(data = await client.ReceiveDataAsync()))
                    {
                        foreach (var s in data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var ma = _hotspotRegEx.Match(s);
                            if (ma.Success)
                            {
                                var id = ma.Groups["ssid"].Value;
                                var hotspot = hostspots.Find((h) => { return h.BSSID == id; });
                                if (hotspot == null)
                                {
                                    hotspot = new LEDHotspot()
                                    {
                                        BSSID = ma.Groups["ssid"].Value,
                                        Channel = byte.Parse(ma.Groups["ch"].Value),
                                        DPID = ma.Groups["dpid"].Value,
                                        ExtCH = ma.Groups["extch"].Value,
                                        NT = ma.Groups["nt"].Value,
                                        Security = ma.Groups["security"].Value,
                                        Signal = byte.Parse(ma.Groups["signal"].Value),
                                        SSID = ma.Groups["ssid"].Value,
                                        WPS = ma.Groups["wps"].Value
                                    };
                                    hostspots.Add(hotspot);
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                client.Close();
            }
            return hostspots;
        }
        public static async Task<string> VersionAsync(string bridgeIP)
        {
            var client = new LEDAdminClient();
            try
            {
                if (await HandshakeAsync(client))
                {
                    await client.SendDataAsync("AT+VER\r\n");
                    var data = string.Empty;
                    while (!string.IsNullOrEmpty(data = await client.ReceiveDataAsync()))
                    {
                        var ma = _versionRegEx.Match(data);
                        if (ma.Success)
                            return ma.Groups["ver"].Value;
                    }
                }
            }
            finally
            {
                client.Close();
            }
            return string.Empty;
        }
        public static async Task<bool> SetupHotspotAsync(string bridgeIP, string ssid, string password)
        {
            var client = new LEDAdminClient();
            try
            {
                if (await HandshakeAsync(client))
                {
                    await client.SendDataAsync(string.Format("AT+WSSSID={0}\r\n", ssid));
                    if (await ReceiveOk(client))
                    {
                        await client.SendDataAsync(string.Format("AT+WSKEY={0},{1},{2}\r\n", "WPA2PSK", "AES", password));
                        if (await ReceiveOk(client))
                        {
                            await client.SendDataAsync(string.Format("AT+WMODE=STA\r\n", ssid));
                            if (await ReceiveOk(client))
                            {
                                await client.SendDataAsync(string.Format("AT+Z\r\n", ssid));
                                if (await ReceiveOk(client))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                client.Close();
            }
            return false;
        }
        private static async Task<bool> HandshakeAsync(LEDAdminClient client)
        {
            await client.SendDataAsync("Link_Wi-Fi");
            var ok = false;
            var data = string.Empty;
            while (!string.IsNullOrEmpty(data = await client.ReceiveDataAsync()))
            {
                var ma = _IPIdRegEx.Match(data);
                if (ma.Success)
                {
                    ok = true;
                    break;
                }
            }
            if (ok)
            {
                await client.SendDataAsync("+ok");
                return true;
            }
            else
            {
                return false;
            }
        }
        private static async Task<bool> ReceiveOk(LEDAdminClient client)
        {
            var s = await client.ReceiveDataAsync();
            return s.Contains("+ok");
        }
    }
}
