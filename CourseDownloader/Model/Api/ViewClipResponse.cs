namespace CourseDownloader.Model.Api;

public class ViewClipResponse
{
    public required IEnumerable<ClipUrl> Urls { get; init; }
}