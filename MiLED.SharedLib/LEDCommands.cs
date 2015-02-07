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
    public class LEDCommands
    {
        // White LEDs
        public static readonly byte[] AllOn = new byte[] { 0x35, 0x0, 0x55 };
        public static readonly byte[] AllOff = new byte[] { 0x39, 0x0, 0x55 };
        public static readonly byte[] BrightnessUp = new byte[] { 0x3C, 0x0, 0x55 };
        public static readonly byte[] BrightnessDown = new byte[] { 0x34, 0x0, 0x55 };
        public static readonly byte[] ColorTempDown = new byte[] { 0x3F, 0x0, 0x55 };
        public static readonly byte[] Group1On = new byte[] { 0x38, 0x0, 0x55 };
        public static readonly byte[] Group1Off = new byte[] { 0x3B, 0x0, 0x55 };
        public static readonly byte[] Group2On = new byte[] { 0x3D, 0x0, 0x55 };
        public static readonly byte[] Group2Off = new byte[] { 0x33, 0x0, 0x55 };
        public static readonly byte[] Group3On = new byte[] { 0x37, 0x0, 0x55 };
        public static readonly byte[] Group3Off = new byte[] { 0x3A, 0x0, 0x55 };
        public static readonly byte[] Group4On = new byte[] { 0x32, 0x0, 0x55 };
        public static readonly byte[] Group4Off = new byte[] { 0x36, 0x0, 0x55 };
        public static readonly byte[] AllFull = new byte[] { 0xB5, 0x0, 0x55 };
        public static readonly byte[] Group1Full = new byte[] { 0xB8, 0x0, 0x55 };
        public static readonly byte[] Group2Full = new byte[] { 0xBD, 0x0, 0x55 };
        public static readonly byte[] Group3Full = new byte[] { 0xB7, 0x0, 0x55 };
        public static readonly byte[] Group4Full = new byte[] { 0xB2, 0x0, 0x55 };
        public static readonly byte[] AllNight = new byte[] { 0xB9, 0x0, 0x55 };
        public static readonly byte[] Group1Night = new byte[] { 0xBB, 0x0, 0x55 };
        public static readonly byte[] Group2Night = new byte[] { 0xB3, 0x0, 0x55 };
        public static readonly byte[] Group3Night = new byte[] { 0xBA, 0x0, 0x55 };
        public static readonly byte[] Group4Night = new byte[] { 0xB6, 0x0, 0x55 };
        public static readonly byte[] ColorTempUp = new byte[] { 0x3E, 0x0, 0x55 };
        // RGB LEDs
        public static readonly byte[] RGBOn = new byte[] { 0x22, 0x0, 0x55 };
        public static readonly byte[] RGBOff = new byte[] { 0x21, 0x0, 0x55 };
        public static readonly byte[] RGBBrightnessUp = new byte[] { 0x23, 0x0, 0x55 };
        public static readonly byte[] RGBBrightnessDown = new byte[] { 0x24, 0x0, 0x55 };
        public static readonly byte[] RGBSpeedUp = new byte[] { 0x25, 0x0, 0x55 };
        public static readonly byte[] RGBSpeedDown = new byte[] { 0x26, 0x0, 0x55 };
        public static readonly byte[] RGBDiscoNext = new byte[] { 0x27, 0x0, 0x55 };
        public static readonly byte[] RGBDiscoLast = new byte[] { 0x28, 0x0, 0x55 };
        public static readonly byte[] RGBColour = new byte[] { 0x20, 0x0, 0x55 };
        //RGBW LEDs   
        public static readonly byte[] RGBWOff = new byte[] { 0x41, 0x0, 0x55 };
        public static readonly byte[] RGBWOn = new byte[] { 0x42, 0x0, 0x55 };
        public static readonly byte[] RGBWDiscoSpeedSlower = new byte[] { 0x43, 0x0, 0x55 };
        public static readonly byte[] RGBWDiscoSpeedFaster = new byte[] { 0x44, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup1AllOn = new byte[] { 0x45, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup1AllOff = new byte[] { 0x46, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup2AllOn = new byte[] { 0x47, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup2AllOff = new byte[] { 0x48, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup3AllOn = new byte[] { 0x49, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup3AllOff = new byte[] { 0x4A, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup4AllOn = new byte[] { 0x4B, 0x0, 0x55 };
        public static readonly byte[] RGBWGroup4AllOff = new byte[] { 0x4C, 0x0, 0x55 };
        public static readonly byte[] RGBWDiscoMode = new byte[] { 0x4D, 0x0, 0x55 };
        public static readonly byte[] SetColorToWhite = new byte[] { 0xC2, 0x0, 0x55 };
        public static readonly byte[] SetColorToWhiteGroup1 = new byte[] { 0xC5, 0x0, 0x55 };
        public static readonly byte[] SetColorToWhiteGroup2 = new byte[] { 0xC7, 0x0, 0x55 };
        public static readonly byte[] SetColorToWhiteGroup3 = new byte[] { 0xC9, 0x0, 0x55 };
        public static readonly byte[] SetColorToWhiteGroup4 = new byte[] { 0xCB, 0x0, 0x55 };
        public static readonly byte[] RGBWBrightness = new byte[] { 0x4E, 0x0, 0x55 };
        public static readonly byte[] RGBWColor = new byte[] { 0x40, 0x0, 0x55 };
    }
}
