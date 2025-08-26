using Soundpad.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Soundpad.Services;

public class ConfigurationService
{
    private const string DefaultConfigName = "appconfig.json";
    private string _configPath;

    public ConfigurationService()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string AppFolderPath = Path.Combine(appDataPath, "Soundpad");
        _configPath = Path.Combine(AppFolderPath, DefaultConfigName);

        if (!Directory.Exists(AppFolderPath))
        {
            Directory.CreateDirectory(AppFolderPath);
        }
    }

    public bool IsFirstRun()
    {
        return !File.Exists(_configPath);
    }

    public async Task<AppConfig> LoadConfigAsync()
    {
        if (IsFirstRun())
        {
            return await CreateDefaultCofigAsync();
        }

        try
        {
            var json = await File.ReadAllTextAsync(_configPath);
            var config = JsonSerializer.Deserialize<AppConfig>(json);
            config.ConfigPath = _configPath;
            return config;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return await CreateDefaultCofigAsync();
        }
    }

    public async Task SaveConfigAsync(AppConfig config)
    {
        try
        {
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            var json = JsonSerializer.Serialize(config, options);
            await File.WriteAllTextAsync(_configPath, json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    private async Task<AppConfig> CreateDefaultCofigAsync()
    {
        var defaultConfig = new AppConfig
        {
            ConfigPath = _configPath
        };

        await SaveConfigAsync(defaultConfig);
        return defaultConfig;
    }
}
