using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Soundpad.Configuration;
using Soundpad.Models;
using Soundpad.Services;
using System.Diagnostics;
using System.Reactive;

namespace Soundpad.ViewModels;

public class MainViewModel : ReactiveObject
{
    private readonly SoundsManagerService _soundsManagerService;
    public SoundsManagerService SoundsManagerService => _soundsManagerService;
    [Reactive] public Category SelectedCategory { get; set; }

    public ReactiveCommand<string, Unit> OpenUrlCommand { get; }

    public MainViewModel(AppConfig config)
    {
        _soundsManagerService = new(config);

        if (SoundsManagerService != null)
        {
            SelectedCategory = _soundsManagerService.Categories[0];
        }

        OpenUrlCommand = ReactiveCommand.Create<string>(OpenUrl);
    }

    private void OpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }
}
