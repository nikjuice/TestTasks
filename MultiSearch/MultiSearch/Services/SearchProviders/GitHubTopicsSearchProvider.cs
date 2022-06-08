using System.Text.Json;
using Microsoft.Net.Http.Headers;
using MultiSearch.Models;
using MultiSearch.Models.ApiResponseModels;

namespace MultiSearch.Services;

public class GitHubTopicsSearchProvider : ISearchProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GitHubTopicsSearchProvider> _logger;
    private readonly HttpClient _httpClient;

    private const string RequestUrl =
        "https://api.github.com/search/topics?q={0}";

    public GitHubTopicsSearchProvider(IHttpClientFactory httpClientFactory, ILogger<GitHubTopicsSearchProvider> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        
        _httpClient = _httpClientFactory.CreateClient();
    }
    public async Task<IResult<int>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Result<int>.Error("Search term is null or empty", -1);
        }
        
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get, string.Format(RequestUrl, searchTerm)
           )
        {
            Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"}
            }
        };

        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
        
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            await using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();
            
            var responseModel = await JsonSerializer.DeserializeAsync
                <GitHubTopicSearchResponseModel>(contentStream);
            if (responseModel != null)
            {
                _logger.LogInformation($"Provider return {responseModel.TotalHits} results for term '{searchTerm}'");
                return Result<int>.OK(responseModel.TotalHits);
            }
            
            return Result<int>.Error("Deserialization error", -1);
        }
        
        var error = $"Failed to get data, status code - {httpResponseMessage.StatusCode}, Reason - {httpResponseMessage.ReasonPhrase}";
        
        _logger.LogError(error);
        return Result<int>.Error(error, -1);

    }
    
    public string Name => "Github topic search";
}