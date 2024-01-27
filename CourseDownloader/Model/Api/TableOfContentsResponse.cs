namespace CourseDownloader.Model.Api;

public class TableOfContentsResponse
{
    public required string Title { get; init; }
    public required IEnumerable<Module> Modules { get; init; }
}