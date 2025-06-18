using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Windows.UI;
using Newtonsoft.Json;
using Microsoft.UI.Xaml;

namespace Sunbeam
{
    public class GlobalItems
    {
        public static GlobalMemory? GlobalMemory { get; set; }
        public static Settings? Settings { get; set; }
    }
    public class Settings
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

        public static Settings LoadSettings()
        {
            string path = "config.json";
            string json = File.ReadAllText(path);
            Settings settings = JsonConvert.DeserializeObject<Settings>(json) ?? new();
            return settings;
        }

        public static void SaveSettings(Settings settings)
        {
            string path = "config.json";
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, json);
        }

    }

    public class GlobalMemory
    {
        public string CurrentFile { get; set; } = string.Empty;
        public string CurrentFileContent { get; set; } = string.Empty;
        public string WrapFix { get; set; } = string.Empty;
        public string[,]? NoteList { get; set; }
        public Window? Window { get; set; }
    }
}