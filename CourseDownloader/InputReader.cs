using CourseDownloader.Model;

namespace CourseDownloader;

public static class InputReader
{
    public static Input Read()
    {
        var courseUrl = ReadCourseUrl();
        var sessionId = ReadSessionId();
        var (clipStart, clipEnd) = ReadClipRange();

        return new Input
        {
            SessionId = sessionId,
            CourseUrl = courseUrl,
            ClipNumberStart = clipStart,
            ClipNumberEnd = clipEnd
        };
    }

    private static (int clipStart, int? clipEnd) ReadClipRange()
    {
        Console.Write("Enter starting clip number (default = 1): ");
        var clipStart = Console.ReadLine();
        var clipStartParsed = string.IsNullOrEmpty(clipStart) ? 1 : int.Parse(clipStart);

        Console.Write("Enter ending clip number (default = all): ");
        var clipEnd = Console.ReadLine();
        int? clipEndParsed = string.IsNullOrEmpty(clipEnd) ? null : int.Parse(clipEnd);
        return (clipStartParsed, clipEndParsed);
    }

    private static string ReadSessionId()
    {
        Console.Write("Enter 'Session' cookie value: ");
        var sessionId = Console.ReadLine();
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ArgumentException("Session ID cannot be empty.");
        }

        return sessionId;
    }

    private static string ReadCourseUrl()
    {
        Console.Write("Enter Course Player URL: ");
        var courseUrl = Console.ReadLine();
        if (string.IsNullOrEmpty(courseUrl))
        {
            throw new ArgumentException("Course URL cannot be empty.");
        }

        return courseUrl;
    }
}