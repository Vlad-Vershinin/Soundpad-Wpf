using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Diagnostics;
using TagLib;

namespace Soundpad.Models;

public class Sound : ReactiveObject
{
    public string Name { get; }
    public TimeSpan Duration { get; }
    public string Path { get; }
    public string Author { get; }

    public Sound(string path)
    {
        Path = path;

        var file = File.Create(path);

        Name = file.Name;
        Duration = file.Properties.Duration;
        Author = file.Tag.FirstPerformer;
    }
}
