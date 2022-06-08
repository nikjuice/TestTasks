using System.Text.RegularExpressions;
using Microsoft.Net.Http.Headers;
using MultiSearch.Models;

namespace MultiSearch.Services;

public class BingSearchProvider : ISearchProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<BingSearchProvider> _logger;
    private readonly HttpClient _httpClient;

    private const string RequestUrl =
        "https://www2.bing.com/search?q={0}";

    public BingSearchProvider(IHttpClientFactory httpClientFactory, ILogger<BingSearchProvider> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        
        _httpClient = _httpClientFactory.CreateClient();
        
        //Faking chrome to get more accurate html
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent,  "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.64 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept,  "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.AcceptLanguage,  "en-US,en;q=0.9,ru;q=0.8,uk;q=0.7");
        
        
    }
    public async Task<IResult<int>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Result<int>.Error("Search term is null or empty", -1);
        }
        
        var httpResponseMessage = await _httpClient.GetAsync(string.Format(RequestUrl, searchTerm));
        
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var pageContents = await httpResponseMessage.Content.ReadAsStringAsync();
            var pattern = "<span class=\"sb_count\".*?>(.*?)</span>";
            var options = RegexOptions.Multiline;
            var match = Regex.Matches(pageContents, pattern, options);

            if (match.Count == 1)
            {
                var result  = ParseNumber(match.First().Groups[1].Value);
                
                _logger.LogInformation($"Provider returns {result} results for term '{searchTerm}'");
                return Result<int>.OK(result);    
            }

            return Result<int>.Error("Can't grab data from html", -1);

        }
      
        var error = $"Failed to get data, status code - {httpResponseMessage.StatusCode}, Reason - {httpResponseMessage.ReasonPhrase}";
        
        _logger.LogError(error);
        return Result<int>.Error(error, -1);

    }

    private static int ParseNumber(string value)
    {
        value = Regex.Replace(value, @"&#\d*;", "");
        value = value.Replace(" ", string.Empty);
        value = value.Replace("results", string.Empty);

        return int.Parse(value);
    }

    public string Name => "Bing";
}