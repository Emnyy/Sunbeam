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
        public GlobalMemory GlobalMemory { get; set; }
        public MainWindow()
        {
            GlobalMemory = ((App)Application.Current).GlobalMemory;
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            var displayArea = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Primary);
            var wSize = 0.7 * displayArea.WorkArea.Height;

            var newSize = new Windows.Graphics.SizeInt32((int)(wSize), (int)(wSize));
            AppWindow.Resize(newSize);

            MainFrame.Navigate(typeof(NotesMainPage));

            var present = AppWindow.Presenter as OverlappedPresenter;
            if (present != null)
            {
                present.IsAlwaysOnTop = true;
            }


            if (MainFrame.Content is Page page)
            {
                SetTitleBar(page.FindName("TopBar") as UIElement);
                if (page.FindName("Exit") is Border exit)
                {
                    exit.PointerPressed += CloseWindow;
                }
            }

            Closed += Window_SaveFile;
            Activated += Window_SaveFile;
        }

        private void Window_SaveFile(object sender, WindowActivatedEventArgs args)
        {
            if (MainFrame.Content is Page page)
            {
                if (page.FindName("MainTextArea") is TextBox textBox)
                {
                    File.WriteAllText(GlobalMemory.CurrentFile ?? "Notes\\" + DateTime.Now.ToString() + ".txt", textBox.Text);
                }
            }
        }

        private void Window_SaveFile(object sender, WindowEventArgs e)
        {
            if (MainFrame.Content is Page page)
            {
                if(page.FindName("MainTextArea") is TextBox textBox)
                {
                    File.WriteAllText(GlobalMemory.CurrentFile ?? "Notes\\" + DateTime.Now.ToString() + ".txt", textBox.Text);
                }
            }
        }

        private void CloseWindow(object sender, PointerRoutedEventArgs e)
        {
            Close();
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            if (MainFrame.Content is Page page)
            {
                SetTitleBar(page.FindName("TopBar") as UIElement);
            }
        }
    }
}
