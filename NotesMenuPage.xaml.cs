using ABI.Windows.UI;
using CommunityToolkit.WinUI;
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

            GetNoteList();
        }

        void GetNoteList()
        {
            GlobalMemory.NoteList = GetFileInfo(false, String.Empty);

            if (GlobalMemory.NoteList != null)
            {
                for (int i = 0; i < GlobalMemory.NoteList.GetLength(1); i++)
                {
                    var stackpanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    var favoriteIcon = new FontIcon
                    {
                        Glyph = "\uE734",
                        FontSize = 16,
                        Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["SystemAccentColor"])
                    };

                    var deleteIcon = new FontIcon
                    {
                        Glyph = "\uE74D",
                        FontSize = 16,
                        Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["SystemAccentColor"])
                    };

                    var Border1 = new Border
                    {
                        Margin = new Thickness(0, 0, 16, 0),
                    };
                    var Border2 = new Border { };

                    var settingsCard = new SettingsCard
                    {
                        IsClickEnabled = true,
                        IsActionIconVisible = false,
                        Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["SystemAccentColor"]),
                        Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["SystemAccentColorLight3"]),
                        Margin = new Thickness(0, 0, 0, 4),
                        Header = GlobalMemory.NoteList[1, i],
                        Tag = i,
                        Content = stackpanel
                    };

                    settingsCard.Click += SettingsCard_Click;

                    Border1.PointerPressed += ToggleFavortie;
                    Border2.PointerPressed += DeleteNote;

                    Border1.Child = favoriteIcon;
                    Border2.Child = deleteIcon;

                    stackpanel.Children.Add(Border1);
                    stackpanel.Children.Add(Border2);

                    NoteList.Children.Add(settingsCard);
                }
            }
        }

        private void DeleteNote(object sender, PointerRoutedEventArgs e)
        {
            if (GlobalMemory.NoteList != null && sender is Border border && border.Parent is StackPanel stackPanel && stackPanel.Parent is SettingsCard settingsCard)
            {
                File.Delete("Notes\\" + GlobalMemory.NoteList[1, (int)settingsCard.Tag] + ".txt");
                GlobalMemory.NoteList[1, (int)settingsCard.Tag] = String.Empty;
                GlobalMemory.NoteList[0, (int)settingsCard.Tag] = String.Empty;
                //GlobalMemory.NoteList = GetFileInfo(false, String.Empty);
                NoteList.Children.Remove(settingsCard);
                e.Handled = true;
            }
        }

        private void SettingsCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is SettingsCard card)
            {
                Frame.Navigate(typeof(NotesMainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

                if(GlobalMemory.NoteList == null) { return; }
                GlobalMemory.CurrentFile = GlobalMemory.NoteList[1, (int)card.Tag];
                GlobalMemory.CurrentFileContent = File.ReadAllText("Notes\\" + GlobalMemory.CurrentFile + ".txt");
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
                if(border.Parent is StackPanel stackPanel && stackPanel.Parent is SettingsCard)
                {
                    e.Handled = true;
                }
            }
        }

        private static string[,] GetFileInfo(bool CheckString, string query)
        {
            string[] files = Directory.GetFiles("Notes", "*.txt");
            int i = files.Length;
            string[,] output = new string[2, i];

            for (int x = 0; x < i; x++)
            {
                output[0, x] = File.GetLastWriteTime(files[x]).ToShortDateString();
                output[1, x] = files[x][6..^4];
            }
            if (CheckString)
            {
                var filteredData = Enumerable.Range(0, output.GetLength(1))
                                             .Where(a => output[1, a].StartsWith(query, StringComparison.OrdinalIgnoreCase))
                             .Select(a => new { Time = output[0, a], File = output[1, a] })
                             .ToList();

                if (filteredData.Count == 0)
                {
                    return new string[0, 0];
                }

                output = new string[2, filteredData.Count];
                for (int j = 0; j < filteredData.Count; j++)
                {
                    output[0, j] = filteredData[j].Time;
                    output[1, j] = filteredData[j].File;
                }
            }
            return output;
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string query = Search.Text.ToLowerInvariant();
            NoteList.Children.Clear();

            GlobalMemory.NoteList = GetFileInfo(true, query);
            if (GlobalMemory.NoteList.GetLength(1) == 0)
            {
               return;
            }

            GetNoteList();

        }
    }
}
