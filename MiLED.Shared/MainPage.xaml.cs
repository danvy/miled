using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MiLED
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LEDBridge _Bridge = null;
        public MainPage()
        {
            this.InitializeComponent();
            _Bridge = new LEDBridge("10.10.100.254", "8899");
        }
        private async void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchButton.IsChecked == true)
            {
                await _Bridge.SendCommandAsync(LEDCommands.RGBWOn);
                for (byte c = 0x60; c < 0xB0; c++)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(200));
                    await _Bridge.RGBWColorAsync(c);
                }
            }
            else
            {
                await _Bridge.SendCommandAsync(LEDCommands.RGBWOff);
            }
        }
    }
}
