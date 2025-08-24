using ReactiveUI;
using System.Diagnostics;
using System.Reactive;

namespace Soundpad.ViewModels;

public class MainViewModel : ReactiveObject
{
    public ReactiveCommand<string, Unit> OpenUrlCommand { get; }

    public MainViewModel()
    {
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
