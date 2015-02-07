// Coded by Alex Danvy
// http://danvy.tv
// http://twitter.com/danvy
// http://github.com/danvy
// Licence Apache 2.0
// Use at your own risk, have fun

using System;
using System.Collections.Generic;
using System.Text;

namespace MiLED
{
    public class LEDHotspot
    {
        public byte Channel {get; set; }
        public string SSID {get; set; }
        public string BSSID {get; set; }
        public string Security {get; set; }
        public byte Signal {get; set; }
        public string ExtCH {get; set; }
        public string NT {get; set; }
        public string WPS {get; set; }
        public string DPID { get; set; }
    }
}
