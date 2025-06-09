using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using System.Threading;

namespace Sunbeam
{
    public static class WebHelper
    {
        public static void CheckUpdate(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                button.IsEnabled = false;
            }

            Thread.Sleep(2000);

            if (button != null)
            {
                button.IsEnabled = true;
            }
        }

        /*static async Task<int> CheckForUpdatesAsync()
        {
            try
            {
                using HttpClient client = new();
                string url = "https://api.github.com/repos/REDACTED_PROJECT_NAME/releases/latest";
                var response = await client.GetStringAsync(url);
                var releaseInfo = JsonSerializer.Deserialize<ReleaseInfo>(response);
                if (releaseInfo != null && !string.IsNullOrEmpty(releaseInfo.TagName))
                {
                    return releaseInfo.TagName.CompareTo("v1.0.0");
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }*/
    }
}
