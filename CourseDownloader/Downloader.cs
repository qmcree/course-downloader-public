using CourseDownloader.Model;
using CourseDownloader.Model.Api;

namespace CourseDownloader;

public class Downloader
{
    private const string MediaType = "mp4";
    
    public required CourseApiClient Client { get; init; }
    public required FileRepository FileRepository { get; init; }
    public required string CourseId { get; init; }

    public async Task DownloadCourseClips(int clipStart, int? clipEnd)
    {
        Console.WriteLine($"Downloading course clips starting from {clipStart} to {(clipEnd is null ? "all" : clipEnd)}.");
        var clips = await GetClips(clipStart, clipEnd);
        Console.WriteLine($"Retrieved details of {clips.Count} clips.");

        for (var index = 0; index < clips.Count; index++)
        {
            var clip = clips[index];
            await DownloadClip(clip);
            Console.WriteLine($"#{index + 1} of {clips.Count}: Downloaded clip '{clip.Label}'.");
            // Prevent being rate limited if there are more clips.
            if (clips.ElementAtOrDefault(index + 1) is not null) await Task.Delay(1000);
        }
    }

    private async Task DownloadClip(Clip clip)
    {
        var urls = (await Client.GetVideoUrls(new ViewClipRequest
        {
            ClipId = clip.ClipId,
            VersionId = clip.VersionId,
            MediaType = MediaType,
            Quality = "800x600",
        })).ToArray();
        if (!urls.Any())
        {
            throw new InvalidDataException($"No video URLs returned for clip '{clip.DebugString}'.");
        }
        Console.WriteLine($"Retrieved video URLs of '{clip.Label}'.");
        // The first one has the highest rank.
        await using var stream = await Client.GetVideo(urls[0]);
        await FileRepository.SaveClip(stream, clip, MediaType);
    }

    private async Task<IReadOnlyList<Clip>> GetClips(int clipStart, int? clipEnd)
    {
        var contents = await Client.GetTableOfContents(CourseId);
        var clips = new List<Clip>();
        var count = 0;
        foreach (var module in contents.Modules)
        {
            foreach (var item in module.ContentItems)
            {
                if (item.Type != "clip") continue;
                count++;
                if (clipStart > count) continue;
                
                clips.Add(new Clip
                {
                    ClipId = item.Id,
                    VersionId = item.Version,
                    Position = count,
                    CourseTitle = contents.Title,
                    ModuleTitle = module.Title,
                    ClipTitle = item.Title,
                });
                if (clipEnd <= count) return clips;
            }
        }
        return clips;
    }
}