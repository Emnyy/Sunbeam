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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sunbeam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesMenuPage : Page
    {
        public NotesMenuPage()
        {
            InitializeComponent();
        }
        private void NoteHover(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Border border && border.Child is FontIcon icon)
            {
                if (Application.Current.Resources["TextOnAccentFillColorSecondaryBrush"] is SolidColorBrush brush)
                {
                    icon.Foreground = new SolidColorBrush(brush.Color);
                }
            }
            else if (sender is Border border1 && border1.Child is TextBlock block)
            {
                if (Application.Current.Resources["TextOnAccentFillColorSecondaryBrush"] is SolidColorBrush brush)
                {
                    block.Foreground = new SolidColorBrush(brush.Color);
                }
            }
        }
        private void NoteHoverStop(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Border border && border.Child is FontIcon icon)
            {
                if (Application.Current.Resources["TextOnAccentFillColorPrimaryBrush"] is SolidColorBrush brush)
                {
                    icon.Foreground = new SolidColorBrush(brush.Color);
                }
            }
            else if (sender is Border border1 && border1.Child is TextBlock block)
            {
                if (Application.Current.Resources["TextOnAccentFillColorPrimaryBrush"] is SolidColorBrush brush)
                {
                    block.Foreground = new SolidColorBrush(brush.Color);
                }
            }
        }

        private void TitleHover(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Border border1 && border1.Child is TextBlock block)
            {
                if (Application.Current.Resources["TextOnAccentFillColorSecondaryBrush"] is SolidColorBrush brush)
                {
                    block.Foreground = new SolidColorBrush(brush.Color);
                    block.TextDecorations = Windows.UI.Text.TextDecorations.Underline;
                }
            }
        }

        private void TitleHoverStop(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Border border1 && border1.Child is TextBlock block)
            {
                if (Application.Current.Resources["TextOnAccentFillColorPrimaryBrush"] is SolidColorBrush brush)
                {
                    block.Foreground = new SolidColorBrush(brush.Color);
                    block.TextDecorations = Windows.UI.Text.TextDecorations.None;
                }
            }
        }
        private void ReturnToNotes(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesMainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
        private void ToggleFavortie(object sender, PointerRoutedEventArgs e)
        {
            if (Favorites.Glyph != "\uE735")
            {
                Favorites.Glyph = "\uE735";
            }
            else
            {
                Favorites.Glyph = "\uE734";
            }
        }
    }
}
