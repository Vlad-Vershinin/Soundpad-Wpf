using Soundpad.Configuration;
using Soundpad.Models;
using System.IO;

namespace Soundpad.Services;

public class SoundsManagerService
{
    public List<Category> Categories { get; set; } = new();

    public SoundsManagerService(AppConfig config)
    {
        List<string> files = new();
        files.AddRange(Directory.GetDirectories(config.SoundsFolder));

        foreach (var file in files)
        {
            Categories.Add(new Category(file));
        }
    }
}
