using Movies.Client.Helpers;
using Movies.Client.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Movies.Client;

public class TestableClassWithApiAccess(HttpClient httpClient,
        JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly JsonSerializerOptionsWrapper _jsonSerializerOptionsWrapper = jsonSerializerOptionsWrapper;

    public async Task<Movie?> GetMovieAsync(
    CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "api/movies/030a43b0-f9a5-405a-811c-bf342524b2be");
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.AcceptEncoding.Add(
            new StringWithQualityHeaderValue("gzip"));

        using (var response = await _httpClient.SendAsync(request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken))
        {
            if (!response.IsSuccessStatusCode)
            {
                // inspect the status code
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // show this to the user
                    Console.WriteLine("The requested movie cannot be found.");
                    return null;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // trigger a login flow 
                    throw new UnauthorizedApiAccessException();
                } 

                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                return await JsonSerializer.DeserializeAsync<Movie>(
                    stream,
                  _jsonSerializerOptionsWrapper.Options,
                  cancellationToken);
            }
        }
        return null;
    }


}
