using Microsoft.UI.Windowing;
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
    public sealed partial class SettingsWindow : Window
    {
        public SettingsViewModel ViewModel { get; set; }
        public SettingsWindow()
        {
            InitializeComponent();
            ViewModel = ((App)Application.Current).ViewModel;
            ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

            var displayArea = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Primary);
            var size = new Windows.Graphics.SizeInt32((int)(0.7 * displayArea.WorkArea.Width), (int)(0.7 * displayArea.WorkArea.Height));
            AppWindow.Resize(size);
            AppWindow.Move(new Windows.Graphics.PointInt32((displayArea.WorkArea.Width - size.Width) / 2, (displayArea.WorkArea.Height - size.Height) / 2));

            SettingsContentFrame.Navigate(typeof(SettingsOptionsPage));
            Closed += Window_SaveSettings;
            Activated += Window_SaveSettings;
        }

        private void Window_SaveSettings(object sender, WindowActivatedEventArgs args)
        {
            if (args.WindowActivationState == WindowActivationState.Deactivated)
            {
                SettingsViewModel.SaveSettings(ViewModel);
            }
        }

        private void Window_SaveSettings(object sender, WindowEventArgs e)
        {
            SettingsViewModel.SaveSettings(ViewModel);
        }



        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                switch (selectedItem.Tag)
                {
                    case "OptionsPage":
                        SettingsContentFrame.Navigate(typeof(SettingsOptionsPage));
                        break;
                    case "FavoritesPage":
                        SettingsContentFrame.Navigate(typeof(SettingsFavoritesPage));
                        break;
                    case "HelpPage":
                        SettingsContentFrame.Navigate(typeof(SettingsHelpPage));
                        break;
                    case "AboutPage":
                        SettingsContentFrame.Navigate(typeof(SettingsAboutPage));
                        break;
                }
            }
        }
    }
}
