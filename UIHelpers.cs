using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunbeam
{
    public static class UIHelpers
    {
        public static void ExitApp(object sender, PointerRoutedEventArgs e)
        {
            App.Current.Exit();
        }
        public static void NoteHover(object sender, PointerRoutedEventArgs e)
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
        public static void NoteHoverStop(object sender, PointerRoutedEventArgs e)
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
    }
}
