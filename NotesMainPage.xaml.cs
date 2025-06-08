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
        }
        private void ExitApp(object sender, PointerRoutedEventArgs e)
        {
            App.Current.Exit();
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
        private void NoteHoverStop(object sender, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
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

        private void TitleHover(object sender, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
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

        private void ListNotes(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesMenuPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
}
}
