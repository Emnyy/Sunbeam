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
        public SettingsWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

            var displayArea = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Primary);
            var size = new Windows.Graphics.SizeInt32((int)(0.7 * displayArea.WorkArea.Width), (int)(0.7 * displayArea.WorkArea.Height));
            AppWindow.Resize(size);
            AppWindow.Move(new Windows.Graphics.PointInt32((int)(displayArea.WorkArea.Width - size.Width) / 2, (int)(displayArea.WorkArea.Height - size.Height) / 2));

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                switch (selectedItem.Tag)
                {
                    case "AppearancePage":
                        SettingsContentFrame.Navigate(typeof(SettingsAppearancePage));
                        break;
                    case "BehaviorPage":
                        SettingsContentFrame.Navigate(typeof(SettingsBehaviorPage));
                        break;
                }
            }
        }
    }
}
