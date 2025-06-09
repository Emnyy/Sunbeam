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
    public sealed partial class NotesMenuPage : Page
    {
        public NotesMenuPage()
        {
            InitializeComponent();
            NoteList.PointerEntered += UIHelpers.NoteHover;
            NoteList.PointerExited += UIHelpers.NoteHoverStop;

            Favorites.PointerEntered += UIHelpers.NoteHover;
            Favorites.PointerExited += UIHelpers.NoteHoverStop;
        }
        
        private void ReturnToNotes(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesMainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
        private void ToggleFavortie(object sender, PointerRoutedEventArgs e)
        {
            if(sender is Border border && border.Child is FontIcon icon)
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
    }
}
