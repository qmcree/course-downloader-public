using CourseDownloader.Model;

namespace CourseDownloader;

public class FileRepository
{
    public async Task SaveClip(Stream stream, Clip clip, string extension)
    {
        var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), clip.CourseTitle);
        CreateDirectory(directoryPath);
        
        await using var file = new FileStream(Path.Combine(directoryPath, $"{clip.Label}.{extension}"), FileMode.CreateNew);
        await stream.CopyToAsync(file);
    }

    private static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}