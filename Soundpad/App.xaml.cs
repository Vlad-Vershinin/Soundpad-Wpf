using Soundpad.Configuration;
using Soundpad.Services;
using Soundpad.Views;
using System.Windows;

namespace Soundpad;

public partial class App : Application
{
    public static AppConfig Config { get; private set; }
    private ConfigurationService _configService;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _configService = new ConfigurationService();
        Config = await _configService.LoadConfigAsync();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (Config != null)
        {
            await _configService.SaveConfigAsync(Config);
        }

        base.OnExit(e);
    }
}

