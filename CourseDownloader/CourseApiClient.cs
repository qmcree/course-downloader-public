using System.Net.Http.Headers;
using System.Net.Http.Json;
using CourseDownloader.Model.Api;

namespace CourseDownloader;

public class CourseApiClient
{
    private HttpClient Client { get; }

    public CourseApiClient(HttpClient client, string sessionId)
    {
        Client = client;
        Client.BaseAddress = new Uri("https://www.example.com"); // Redacted
        Client.DefaultRequestHeaders.Add("Cookie", $"Session={sessionId};");
        Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/601.3.9 (KHTML, like Gecko) Version/9.0.2 Safari/601.3.9");
    }

    public async Task<TableOfContentsResponse> GetTableOfContents(string courseId)
    {
        using var response = await Client.GetAsync($"/course/{courseId}"); // Redacted
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadFromJsonAsync<TableOfContentsResponse>();
        if (responseBody is null)
        {
            throw new NullReferenceException(nameof(responseBody));
        }

        return responseBody;
    }

    public async Task<IEnumerable<string>> GetVideoUrls(ViewClipRequest request)
    {
        // Will return a 403 Forbidden unless the following headers are present. The `Content-Type` cannot have a charset.
        using var requestContent = JsonContent.Create(request, new MediaTypeHeaderValue("application/json"));
        requestContent.Headers.Add("X-Mage", "storm"); // Redacted
        
        using var response = await Client.PostAsync($"/view", requestContent); // Redacted
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadFromJsonAsync<ViewClipResponse>();
        if (responseBody is null)
        {
            throw new NullReferenceException(nameof(responseBody));
        }
        
        return responseBody.Urls.Select(clipUrlInfo => clipUrlInfo.Url);
    }

    public async Task<Stream> GetVideo(string url)
    {
        return await Client.GetStreamAsync(url);
    }
}