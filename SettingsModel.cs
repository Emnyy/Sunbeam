using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunbeam
{
    public class SettingsViewModel
    {
        public int AppTheme { get; set; } = 2;
        public int ColorMode { get; set; } = 0;
        public string CustomColor { get; set; } = "#9359BA"; // Fix: Initialize with a default value

        public int FontSize { get; set; } = 20;
        public bool Wrap { get; set; } = true;

        public int WindowScale { get; set; } = 60;
        public bool AutoStart { get; set; } = true;
                
        public static SettingsViewModel LoadSettings()
        {
            string path = "config.json";
            string json = File.ReadAllText(path);
            SettingsViewModel settingsViewModel = JsonSerializer.Deserialize<SettingsViewModel>(json) ?? new();
            return settingsViewModel;
        }

        //private static readonly JsonSerializerOptions CachedJsonSerializerOptions = new() { WriteIndented = true };

        public static void SaveSettings(SettingsViewModel settings)
        {
            string path = "config.json";
            string json = JsonSerializer.Serialize(settings);
            File.WriteAllText(path, json);
        }
    }
}
