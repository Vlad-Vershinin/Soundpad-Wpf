using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Diagnostics;
using TagLib;

namespace Soundpad.Models;

public class Sound : ReactiveObject
{
    public int Id { get; }
    public string Name { get; }
    public TimeSpan Duration { get; }
    public string Path { get; }
    public string Author { get; }

    public Sound(string path, int id)
    {
        Path = path;

        var file = File.Create(path);

        Id = id;
        Name = file.Name;
        Duration = file.Properties.Duration;
        Author = file.Tag.FirstPerformer;
    }
}
