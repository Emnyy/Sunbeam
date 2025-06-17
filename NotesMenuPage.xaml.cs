using ABI.Windows.UI;
using CommunityToolkit.WinUI.Controls;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

namespace Sunbeam
{
    public sealed partial class NotesMenuPage : Page
    {
        public NotesMenuPage()
        {
            InitializeComponent();
            NoteListButton.PointerEntered += UIHelpers.NoteHover;
            NoteListButton.PointerExited += UIHelpers.NoteHoverStop;

            Favorites.PointerEntered += UIHelpers.NoteHover;
            Favorites.PointerExited += UIHelpers.NoteHoverStop;

            GlobalMemory = ((App)Application.Current).GlobalMemory;

            GlobalMemory.NoteList = GetFileInfo();

            for (int i = 0; i < GlobalMemory.NoteList.GetLength(0) - 1; i++)
            {
                var settingsCard = new SettingsCard
                {
                    IsClickEnabled = true,
                    IsActionIconVisible = false,
                    Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["SystemAccentColor"]),
                    Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["SystemAccentColorLight3"]),
                    Margin = new Thickness(0, 0, 0, 4),
                    Header = GlobalMemory.NoteList[2, i],
                    Tag = i,
                };

                settingsCard.Click += SettingsCard_Click;

                NoteList.Children.Add(settingsCard);
            }
        }

        private void SettingsCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is SettingsCard card)
            {
                Frame.Navigate(typeof(NotesMainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                //GlobalMemory.CurrentFileContent = GlobalMemory.NoteList[(int)card.Tag, 2];
                if(GlobalMemory.NoteList == null) { return; }
                GlobalMemory.CurrentFile = GlobalMemory.NoteList[1, (int)card.Tag];
                GlobalMemory.CurrentFileFriendly = GlobalMemory.CurrentFile[6..^4];
                GlobalMemory.CurrentFileContent = File.ReadAllText(GlobalMemory.CurrentFile);
            }
        }

        public GlobalMemory GlobalMemory { get; set; }

        private void ReturnToNotes(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesMainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void ToggleFavortie(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Border border && border.Child is FontIcon icon)
            {
                if (icon.Glyph != "\uE735")
                {
                    icon.Glyph = "\uE735";
                }
                else
                {
                    icon.Glyph = "\uE734";
                }
            }
        }

        private static string[,] GetFileInfo()
        {
            string[] files = Directory.GetFiles("Notes", "*.txt");
            int i = files.Length;
            string[,] output = new string[3, i];

            for (int x = 0; x < i; x++)
            {
                output[0, x] = File.GetLastWriteTime(files[x]).ToShortDateString();
                output[1, x] = files[x];
                output[2, x] = files[x][6..^4];
            }
            return output;
        }
    }
}
