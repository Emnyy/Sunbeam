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
            ViewModel = new SettingsViewModel();
            InitializeComponent();
        }

        public SettingsViewModel ViewModel { get; set; }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            if (!mutex.WaitOne(0, false))
            {
                _window = new MainWindow();
                _window.Activate();
            }
            else
            {
                ViewModel = SettingsViewModel.LoadSettings();
                _window = new SettingsWindow();
                _window.Activate();
            }
        }
    }
}