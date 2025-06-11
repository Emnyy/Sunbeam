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


namespace Sunbeam
{
    public sealed partial class SettingsOptionsPage : Page
    {
        public SettingsOptionsPage()
        {
            InitializeComponent();
            ViewModel = ((App)Application.Current).ViewModel;
        }

        public SettingsViewModel ViewModel { get; set; }

        private void ColorMode_Changed(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ColorMode = ColorModeControl.SelectedIndex;
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

        private async void ColorPicker_open(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new()
            {
                XamlRoot = this.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                Title = "Choose a custom color",
                PrimaryButtonText = "Done",
                CloseButtonText = "Cancel",
                Content = new SettingsColorDialog()
            };

            switch( await dialog.ShowAsync())
            {
                case ContentDialogResult.Primary:
                    var colorDialog = (SettingsColorDialog)dialog.Content;
                    ViewModel.CustomColor = colorDialog.FindName("ColorWheel") is ColorPicker colorPicker ? colorPicker.Color.ToString() : ViewModel.CustomColor;
                    break;
                default:
                    break;
            }
        }
    }
}