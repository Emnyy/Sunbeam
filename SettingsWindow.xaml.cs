using H.NotifyIcon.Interop;
using Microsoft.UI;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.Win32.UI.WindowsAndMessaging;
using WinRT.Interop;


namespace Sunbeam
{
    public sealed partial class SettingsWindow : Window
    {
        public SettingsViewModel ViewModel { get; set; }

        [LibraryImport("user32.dll")]
        private static partial int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [LibraryImport("user32.dll")]
        private static partial int UnregisterHotKey(IntPtr hWnd, int id);

        [LibraryImport("Comctl32.dll")]
        private static partial int SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, uint uIdSubclass, IntPtr dwRefData);

        [LibraryImport("Comctl32.dll")]
        private static partial IntPtr DefSubclassProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private delegate IntPtr SUBCLASSPROC(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, uint uIdSubclass, IntPtr dwRefData);

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



            uint modifiers = 0;
            uint key = 0;

            string[] parts = ViewModel.RegularNotesShortcut.Split(" + ");
            foreach (string part in parts)
            {
                switch (part)
                {
                    case "Control": modifiers |= 0x0002; break;
                    case "Menu": modifiers |= 0x0001; break;
                    case "Shift": modifiers |= 0x0004; break;
                    case "Windows": modifiers |= 0x0008; break;
                    default:
                        if (Enum.TryParse(part, out VirtualKey virtualKey))
                        {
                            key = (uint)virtualKey;
                        }
                        break;
                }
            }

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            _ = RegisterHotKey(hwnd, 1, modifiers, key);

            parts = ViewModel.FavoriteNotesShortcut.Split(" + ");
            foreach (string part in parts)
            {
                switch (part)
                {
                    case "Control": modifiers |= 0x0002; break;
                    case "Menu": modifiers |= 0x0001; break;
                    case "Shift": modifiers |= 0x0004; break;
                    case "Windows": modifiers |= 0x0008; break;
                    default:
                        if (Enum.TryParse(part, out VirtualKey virtualKey))
                        {
                            key = (uint)virtualKey;
                        }
                        break;
                }
            }
            _ = RegisterHotKey(hwnd, 2, modifiers, key);
            SUBCLASSPROC newWndProc = new(WndProc);
            SetWindowSubclass(hwnd, newWndProc, 1, IntPtr.Zero);
        }

        private IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, uint uIdSubclass, IntPtr dwRefData)
        {
            if (msg == 0x0312)
            {
                switch (wParam.ToInt32())
                {
                    case 1:
                        Window win = new MainWindow();
                        win.Activate();
                        break;
                    case 2:
                        throw new NotImplementedException("Favorite Notes Shortcut is not implemented yet.");
                }
            }
            return DefSubclassProc(hwnd, msg, wParam, lParam);
        }


        private void Window_SaveSettings(object sender, WindowActivatedEventArgs args)
        {
            if (args.WindowActivationState == WindowActivationState.Deactivated)
            {
                SettingsViewModel.SaveSettings(ViewModel);
            }
            _ = UnregisterHotKey(IntPtr.Zero, 1);
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