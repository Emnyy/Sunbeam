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
using Windows.UI;


namespace Sunbeam
{
    public sealed partial class MainWindow : Window
    {
        public GlobalMemory GlobalMemory { get; set; }
        public Settings Settings { get; set; }
        public MainWindow()
        {
            GlobalMemory = ((App)Application.Current).GlobalMemory;
            Settings = ((App)Application.Current).Settings;

            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            var displayArea = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Primary);
            var wSize = (float)Settings.WindowScale / 100 * displayArea.WorkArea.Height;

            var newSize = new Windows.Graphics.SizeInt32((int)(wSize), (int)(wSize));
            AppWindow.Resize(newSize);

            MainFrame.Navigate(typeof(NotesMainPage));

            /*
             * var present = AppWindow.Presenter as OverlappedPresenter;
             *if (present != null)
             *{
             *  present.IsAlwaysOnTop = true;
             *}
            */

            GlobalMemory.Window = this;

            if (MainFrame.Content is Page page)
            {
                SetTitleBar(page.FindName("TopBar") as UIElement);
            }

            Closed += Window_SaveFile;
            Activated += Window_SaveFile;

            if (Settings.ColorMode == 1)
            {
                ColorToHSB((Color)Application.Current.Resources["SystemAccentColor"], out float hue, out float saturation, out float brightness);
                Random Random = new();
                float RandomHue = (float)(Random.NextDouble() * 360);
                MainFrame.Background = new SolidColorBrush(HSBToColor(RandomHue, saturation, brightness));
            }

        }

        static void ColorToHSB(Color color, out float h, out float s, out float b)
        {
            float r = color.R / 255f;
            float g = color.G / 255f;
            float bl = color.B / 255f;

            float max = Math.Max(r, Math.Max(g, bl));
            float min = Math.Min(r, Math.Min(g, bl));
            float delta = max - min;

            h = 0;
            if (delta != 0)
            {
                if (max == r) h = 60 * (((g - bl) / delta) % 6);
                else if (max == g) h = 60 * (((bl - r) / delta) + 2);
                else h = 60 * (((r - g) / delta) + 4);
            }

            if (h < 0) h += 360;
            s = (max == 0) ? 0 : delta / max;
            b = max;
        }

        static Color HSBToColor(float h, float s, float b)
        {
            float c = b * s;
            float x = c * (1 - Math.Abs((h / 60f) % 2 - 1));
            float m = b - c;

            float r = 0, g = 0, bl;

            if (h < 60) { r = c; g = x; bl = 0; }
            else if (h < 120) { r = x; g = c; bl = 0; }
            else if (h < 180) { r = 0; g = c; bl = x; }
            else if (h < 240) { r = 0; g = x; bl = c; }
            else if (h < 300) { r = x; g = 0; bl = c; }
            else { r = c; g = 0; bl = x; }

            return Color.FromArgb(255,
                (byte)((r + m) * 255),
                (byte)((g + m) * 255),
                (byte)((bl + m) * 255));
        }

        private void Window_SaveFile(object sender, WindowActivatedEventArgs args)
        {
            if (MainFrame.Content is Page page)
            {
                if (page.FindName("MainTextArea") is TextBox textBox)
                {
                    File.WriteAllText("Notes\\" + GlobalMemory.CurrentFile + ".txt" ?? "Notes\\" + DateTime.Now.ToString() + ".txt", textBox.Text);
                }
            }
        }

        private void Window_SaveFile(object sender, WindowEventArgs e)
        {
            if (MainFrame.Content is Page page)
            {
                if (page.FindName("MainTextArea") is TextBox textBox)
                {
                    File.WriteAllText("Notes\\" + GlobalMemory.CurrentFile + ".txt" ?? "Notes\\" + DateTime.Now.ToString() + ".txt", textBox.Text);
                }
            }
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            if (MainFrame.Content is Page page)
            {
                SetTitleBar(page.FindName("TopBar") as UIElement);
                if(page.FindName("FileName") is TextBox box)
                {
                    box.MaxWidth = args.Size.Width * 0.7;
                }
            }
        }
    }
}
