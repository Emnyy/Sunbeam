using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Windows.UI;
using Newtonsoft.Json;

namespace Sunbeam
{
    public class SettingsViewModel
    {
        public int AppTheme { get; set; } = 2;
        public int ColorMode { get; set; } = 0;
        public string CustomColor { get; set; } = "#9359BA";

        public int FontSize { get; set; } = 20;
        public bool Wrap { get; set; } = true;

        public int WindowScale { get; set; } = 60;
        //public int WindowOpacity { get; set; } = 100;
        //public bool CompactMode { get; set; } = false;
        public bool AutoStart { get; set; } = true;

        public string RegularNotesShortcut { get; set; } = "Control + Menu + N";
        public string FavoriteNotesShortcut { get; set; } = "Control + Menu + F";

        public static SettingsViewModel LoadSettings()
        {
            string path = "config.json";
            string json = File.ReadAllText(path);
            SettingsViewModel settings = JsonConvert.DeserializeObject<SettingsViewModel>(json) ?? new();
            return settings;
        }

        public static void SaveSettings(SettingsViewModel settings)
        {
            string path = "config.json";
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }

    public class GlobalMemory
    {
        public string? CurrentFile { get; set; }
    }
}