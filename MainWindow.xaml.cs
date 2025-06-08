using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.Graphics.Display;


namespace Sunbeam
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window window = this;
            window.ExtendsContentIntoTitleBar = true;
            window.AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            var displayArea = DisplayArea.GetFromWindowId(window.AppWindow.Id, DisplayAreaFallback.Primary);
            var wSize = 0.7 * displayArea.WorkArea.Height;

            var newSize = new Windows.Graphics.SizeInt32((int)(wSize), (int)(wSize));
            window.AppWindow.Resize(newSize);

            MainFrame.Navigate(typeof(NotesMainPage));
            if (MainFrame.Content is Page page)
            {
                SetTitleBar(page.FindName("TopBar") as UIElement);
            }
        }
    }
}
