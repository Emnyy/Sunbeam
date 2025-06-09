using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sunbeam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsOptionsPage : Page
    {
        public SettingsViewModel ViewModel { get; set; }

        public SettingsOptionsPage()
        {
            ViewModel = SettingsViewModel.LoadSettings();
            InitializeComponent();
        }

        private void ColorMode_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (ColorModeControl.SelectedIndex == 2)
            {
                ColorPicker.IsEnabled = true;
            }
            else
            {
                ColorPicker.IsEnabled = false;
            }
        }
        private void AppTheme_Changed(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
