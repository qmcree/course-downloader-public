namespace CourseDownloader.Model;

public class Input
{
    public required string SessionId { get; init; }
    public required string CourseUrl { get; init; }
    public required int ClipNumberStart { get; init; }
    public int? ClipNumberEnd { get; init; }
}