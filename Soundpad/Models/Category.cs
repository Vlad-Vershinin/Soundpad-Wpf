using System.IO;

namespace Soundpad.Models;

public class Category
{
    public string Name { get; }
    public List<Sound> Sounds { get; } = new();

    public Category(string path)
    {
        Name = Directory.GetParent(path).Name;

        var files = Directory.GetFiles(path);
        foreach(var file in files)
        {
            Sounds.Add(new Sound(file));
        }
    }
}
