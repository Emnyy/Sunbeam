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
            GlobalMemory = ((App)Application.Current).GlobalMemory;

            NoteList.PointerEntered += UIHelpers.NoteHover;
            NoteList.PointerExited += UIHelpers.NoteHoverStop;

            Exit.PointerEntered += UIHelpers.NoteHover;
            Exit.PointerExited += UIHelpers.NoteHoverStop;
        }
        public GlobalMemory GlobalMemory { get; set; }
        private void ListNotes(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesMenuPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
