using Microsoft.UI.Windowing;
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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Sunbeam
{
    public sealed partial class NotesMainPage : Page
    {
        public NotesMainPage()
        {
            InitializeComponent();
            GlobalItems = ((App)Application.Current).GlobalItems;
            GlobalMemory = ((App)Application.Current).GlobalMemory;
            Settings = ((App)Application.Current).Settings;

            if (Settings.Wrap == true) { GlobalMemory.WrapFix = "Wrap"; }
            else { GlobalMemory.WrapFix = "NoWrap"; }

            NoteListButton.PointerEntered += UIHelpers.NoteHover;
            NoteListButton.PointerExited += UIHelpers.NoteHoverStop;

            Exit.PointerEntered += UIHelpers.NoteHover;
            Exit.PointerExited += UIHelpers.NoteHoverStop;
            MainTextArea.Focus(FocusState.Pointer);

            Exit.PointerPressed += Exit_PointerPressed;

            //GlobalMemory.CurrentFileContent = GetFileContent(GlobalMemory.CurrentFile);

        }

        private void Exit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (GlobalMemory.Window == null) { return; }
            GlobalMemory.Window.Close();
        }

        public GlobalItems GlobalItems { get; set; }
        public GlobalMemory GlobalMemory { get; set; }
        public Settings Settings { get; set; }

        private static string GetFileContent(string file)
        {
            if (File.Exists(file))
            {
                return File.ReadAllText(file);
            }
            else
            {
                return string.Empty;
            }
        }

        private void ListNotes(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesMenuPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void FileName_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (args.NewText.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
            {
                args.Cancel = true;
            }
        }
    }
}
