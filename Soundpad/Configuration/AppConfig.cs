using System.Text.Json.Serialization;

namespace Soundpad.Configuration;

public class AppConfig
{
    public string Theme { get; set; } = "Dark";
    public string Language { get; set; } = "en";
    public double Volume { get; set; } = 0.8;

    [JsonIgnore]
    public string ConfigPath { get; set; }
}
