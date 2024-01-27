namespace CourseDownloader.Model.Api;

public class ViewClipRequest
{
    public required string ClipId { get; init; }
    public required string VersionId { get; init; }
    public required string MediaType { get; init; }
    public required string Quality { get; init; }
}