using System.Net;
using CourseDownloader;

// Enable with Burp Suite listening to inspect requests.
const bool withInsecureProxy = false;
var input = InputReader.Read();

using var client = new HttpClient(new HttpClientHandler
{
    Proxy = withInsecureProxy ? new WebProxy("http://127.0.0.1:8080") : null,
    ServerCertificateCustomValidationCallback = withInsecureProxy ? HttpClientHandler.DangerousAcceptAnyServerCertificateValidator : null,
});

var downloader = new Downloader
{
    Client = new CourseApiClient(client, input.SessionId),
    FileRepository = new FileRepository(),
    CourseId = new UrlParser().ParseCourseId(input.CourseUrl)
};

await downloader.DownloadCourseClips(input.ClipNumberStart, input.ClipNumberEnd);