using ReactiveUI;
using TagLib;
using System.IO;

namespace Soundpad.Models;

public class Sound : ReactiveObject
{
    public int Id { get; }
    public string Name { get; }
    public string Duration { get; }
    public string Path { get; }
    public string Author { get; }

    public Sound(string path, int id)
    {
        Path = path;

        var file = TagLib.File.Create(path);

        Id = id;
        Name = System.IO.Path.GetFileNameWithoutExtension(path);
        Duration = file.Properties.Duration.ToString(@"mm\:ss");
        Author = file.Tag.FirstPerformer;
    }
}
