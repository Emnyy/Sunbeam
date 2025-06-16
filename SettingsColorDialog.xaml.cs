using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.UI;


namespace Sunbeam;

public sealed partial class SettingsColorDialog : Page
{
    public Settings Settings { get; set; }

    public SettingsColorDialog()
    {
        Settings = ((App)Application.Current).Settings;
        InitializeComponent();
    }
}