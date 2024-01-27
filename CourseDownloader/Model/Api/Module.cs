namespace CourseDownloader.Model.Api;

public class Module
{
    public required string Title { get; init; }
    public required IEnumerable<ContentItem> ContentItems { get; init; }
}