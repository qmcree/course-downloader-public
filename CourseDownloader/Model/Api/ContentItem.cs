namespace CourseDownloader.Model.Api;

public class ContentItem
{
    public required string Id { get; init; }
    public required string Version { get; init; }
    public required string Title { get; init; }
    public required string Type { get; init; }
}