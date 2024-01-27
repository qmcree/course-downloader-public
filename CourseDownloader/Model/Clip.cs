namespace CourseDownloader.Model;

public class Clip
{
    public required string ClipId { get; init; }
    public required string VersionId { get; init; }
    public required int Position { get; init; }
    public required string CourseTitle { get; init; }
    public required string ModuleTitle { get; init; }
    public required string ClipTitle { get; init; }
    public string Label => $"{Position} - {ModuleTitle} - {ClipTitle}";
    public string DebugString => $"{ClipTitle} ({ClipId})";
}