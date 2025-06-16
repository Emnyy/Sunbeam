using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Data.SqlTypes;

namespace Sunbeam
{
    public partial class App : Application
    {
        private Window? _window;

        private static readonly Mutex mutex = new(true, "SunbeamMutex");

        public App()
        {
            GlobalItems = new GlobalItems();
            GlobalMemory = new GlobalMemory();
            Settings = new Settings();
            InitializeComponent();
        }
        public GlobalItems GlobalItems { get; set; }
        public GlobalMemory GlobalMemory { get; set; }
        public Settings Settings { get; set; }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            string FileName = "Notes";
            int count = 0;

            if (!Directory.Exists(FileName))
            {
                Directory.CreateDirectory(FileName);
            }

            FileName = "Notes\\Unnamed" + count + ".txt";

            while (File.Exists(FileName))
            {
                if (File.ReadAllText(FileName) == String.Empty)
                {
                    count = -1;
                    break;
                }
                FileName = "Notes\\Unnamed" + count + ".txt";
                count++;
                
            }

            if (count != -1) { File.Create(FileName); }

            GlobalMemory.CurrentFile = FileName;
            GlobalMemory.CurrentFileFriendly = FileName[6..^4];

            if (!mutex.WaitOne(0, false))
            {
                _window = new MainWindow();
                _window.Activate();
            }
            else
            {
                GlobalItems.Settings = Settings.LoadSettings();
                _window = new SettingsWindow();
                _window.Activate();
            }
        }
    }
}