using System.IO;
using System.Text.Json.Serialization;

namespace Soundpad.Configuration;

public class AppConfig
{
    public string Theme { get; set; } = "Dark";
    public string Language { get; set; } = "en";
    public string SoundsFolder { get; set; } = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Soundpad", "Sounds");
    public double Volume { get; set; } = 0.8;

    [JsonIgnore]
    public string ConfigPath { get; set; }
}
