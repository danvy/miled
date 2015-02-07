using System;
using System.Collections.Generic;
using System.Net;
#if NETFX_CORE || WINDOWS_PHONE
using Windows.Networking.Sockets;
using Windows.Networking;
using Windows.Storage.Streams;
#else
using System.Net.Sockets;
#endif
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiLED
{
    public class LEDClient
    {
        public const string DefaultOutHost = "10.10.100.254";
        public const string DefaultOutService = "48899";
        private static readonly object _lock = new object();
        protected StringBuilder _receiveBuffer = new StringBuilder();
        private string _outHost;
        private string _outService;
        internal string _inHost;
        internal string _inService;
        internal bool _admin = false;
#if NETFX_CORE || WINDOWS_PHONE
        private DatagramSocket _socket;
        private DataWriter _writer;
#else
        protected UdpClient _client;
        private IPEndPoint _outIP;
        internal IPEndPoint _inIP;
#endif
        public LEDClient(string outHost = DefaultOutHost, string outService = DefaultOutService)
        {
            _outHost = outHost;
            _outService = outService;
        }
        public async Task SendDataAsync(string data, int delay = 100)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            await SendDataAsync(dataBytes, delay);
        }
        public async Task SendDataAsync(byte[] data, int delay = 100)
        {
#if NETFX_CORE || WINDOWS_PHONE
            if (_socket == null)
            {
                lock (_lock)
                {
                    if (_socket == null)
                        _socket = new DatagramSocket();
                }
                await _socket.ConnectAsync(new HostName(_outHost), _outService);
                if (_admin)
                    _socket.MessageReceived += _socket_MessageReceived;
                _writer = new DataWriter(_socket.OutputStream);
            }
            _writer.WriteBytes(data);
            await _writer.StoreAsync();
#else
            if (_client == null)
            {
                lock (_lock)
                {
                    if (_client == null)
                        _client = new UdpClient();
                }
                _outIP = new IPEndPoint(IPAddress.Parse(_outHost), int.Parse(_outService));
                _client.Client.MulticastLoopback = false;
                _client.EnableBroadcast = true;
                if (_admin)
                {
                    _inIP = new IPEndPoint(IPAddress.Parse(_inHost), int.Parse(_inService));
                    _client.Client.ReceiveTimeout = 3000;
                    _client.Client.Bind(_inIP);
                }
            }
            _client.Send(data, data.Length, _outIP);
#endif
            await DelayAsync(delay);
        }

#if NETFX_CORE || WINDOWS_PHONE
        private void _socket_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            var reader = args.GetDataReader();
            _receiveBuffer.AppendLine(reader.ReadString(reader.UnconsumedBufferLength));
        }
#endif
        public void Close()
        {
#if NETFX_CORE || WINDOWS_PHONE
            _socket.Dispose();
#else
            if (_client != null)
                _client.Close();
#endif
        }
        public async Task DelayAsync(int duration)
        {
            if (duration > 0)
            {
                await Task.Delay(duration);
            }
        }
    }
}
