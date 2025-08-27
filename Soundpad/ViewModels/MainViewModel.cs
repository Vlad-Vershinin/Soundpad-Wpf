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
    private AudioPlayerService _audioPlayerService;
    public SoundsManagerService SoundsManagerService => _soundsManagerService;
    [Reactive] public Category SelectedCategory { get; set; }
    [Reactive] public Sound SelectedSound { get; set; }

    public ReactiveCommand<string, Unit> OpenUrlCommand { get; }
    public ReactiveCommand<Sound, Unit> PlaySoundCommand { get; }

    public MainViewModel(AppConfig config)
    {
        _soundsManagerService = new(config);
        _audioPlayerService = new AudioPlayerService();

        if (SoundsManagerService != null)
        {
            SelectedCategory = _soundsManagerService.Categories[0];
        }

        OpenUrlCommand = ReactiveCommand.Create<string>(OpenUrl);
        PlaySoundCommand = ReactiveCommand.CreateFromTask<Sound>(PlaySound);
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

    private async Task PlaySound(Sound sound)
    {
        _audioPlayerService.Play(sound);
    }
}
