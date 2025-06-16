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
using Windows.System;
using Windows.UI;
using Windows.UI.Core;


namespace Sunbeam
{
    public sealed partial class SettingsOptionsPage : Page
    {
        public SettingsOptionsPage()
        {
            InitializeComponent();
            Settings = ((App)Application.Current).Settings;
        }

        public Settings Settings { get; set; }

        private void ColorMode_Changed(object sender, SelectionChangedEventArgs e)
        {
            Settings.ColorMode = ColorModeControl.SelectedIndex;
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

            switch (await dialog.ShowAsync())
            {
                case ContentDialogResult.Primary:
                    var colorDialog = (SettingsColorDialog)dialog.Content;
                    Settings.CustomColor = colorDialog.FindName("ColorWheel") is ColorPicker colorPicker ? colorPicker.Color.ToString() : Settings.CustomColor;

                    Binding binding = new()
                    {
                        Source = Settings,
                        Path = new PropertyPath("CustomColor")
                    };
                    ColorPicker.SetBinding(Control.BackgroundProperty, binding);
                    break;
                default:
                    break;
            }
        }
        private VirtualKey[] LastKeyPressed = [];
        private int NumberOfKeysPressed;
        private void ShortcutBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            TextBox? box = sender as TextBox;
            if (box != null)
            {
                if (e.Key == VirtualKey.Escape || e.Key == VirtualKey.Enter || NumberOfKeysPressed == 4)
                {
                    ThisPage.Focus(FocusState.Pointer);
                    return;
                }
                if (LastKeyPressed.Contains(e.Key))
                {
                    return;
                }
                NumberOfKeysPressed++;
                if (box.Text != string.Empty)
                {
                    box.Text += " + ";
                }
                box.Text += e.OriginalKey.ToString();

                // Add the key to the array
                var updatedKeys = new List<VirtualKey>(LastKeyPressed) { e.Key };
                LastKeyPressed = [.. updatedKeys];
            }
        }
                
        private void ShortcutBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? box = sender as TextBox;
            if (box != null)
            {
                box.Text = string.Empty;
                NumberOfKeysPressed = 0;
                LastKeyPressed = [];
            }
        }

        private void FavoriteShortcutBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NumberOfKeysPressed = 0;
            LastKeyPressed = [];

        }
        private void RegularShortcutBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NumberOfKeysPressed = 0;
            LastKeyPressed = [];

        }
    }
}