using System.Text.Json;
using Microsoft.Net.Http.Headers;
using MultiSearch.Exceptions;
using MultiSearch.Models;
using MultiSearch.Models.ApiResponseModels;

namespace MultiSearch.Services;

public class WikipediaSearchProvider : ISearchProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WikipediaSearchProvider> _logger;

    private const string RequestUrl =
        "https://en.wikipedia.org/w/api.php?action=query&list=search&srsearch={0}&utf8=&format=json";

    public WikipediaSearchProvider(IHttpClientFactory httpClientFactory, ILogger<WikipediaSearchProvider> logger)
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
            }
        };

        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
        
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            await using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();
            
            var responseModel = await JsonSerializer.DeserializeAsync
                <WikipediaSearchResponseModel>(contentStream);
            
            if (responseModel != null)
            {
                _logger.LogInformation($"Provider return {responseModel.Query.SearchInfo.TotalHits} results for term '{searchTerm}'");
                return Result<int>.OK(responseModel.Query.SearchInfo.TotalHits);
            }
            
            return Result<int>.Error("Deserialization error", -1);
        }
        
        var error = $"Failed to get data, status code - {httpResponseMessage.StatusCode}, Reason - {httpResponseMessage.ReasonPhrase}";

        return Result<int>.Error(error, -1);
    }
    
    public string Name => "Wikipedia";
}