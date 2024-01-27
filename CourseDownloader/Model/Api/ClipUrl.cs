namespace CourseDownloader.Model.Api;

public class ClipUrl
{
    public required string Cdn { get; init; }
    public required string Url { get; init; }
    public required int Rank { get; init; }
}