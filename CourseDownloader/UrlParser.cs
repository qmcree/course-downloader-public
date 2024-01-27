using System.Text.RegularExpressions;

namespace CourseDownloader;

public partial class UrlParser
{
    [GeneratedRegex(@"(/courses/|id=)(?'id'[\d-a-fA-F]{15})")]
    private static partial Regex CourseUrlRegex();
    
    public string ParseCourseId(string courseUrl)
    {
        var courseId = CourseUrlRegex().Match(courseUrl).Groups["id"].Value;
        if (courseId == string.Empty)
        {
            throw new InvalidDataException("Course URL is not valid.");
        }

        return courseId;
    }
}