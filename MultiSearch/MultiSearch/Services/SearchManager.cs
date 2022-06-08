using MultiSearch.Exceptions;
using MultiSearch.Models;

namespace MultiSearch.Services;

public class SearchManager : ISearchManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SearchManager> _logger;

    public SearchManager(IServiceProvider serviceProvider, ILogger<SearchManager> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task<IList<SearchResponse>> SearchAsync(IList<string> searchTerms)
    {
        if (searchTerms == null || searchTerms.Count == 0)
        {
            throw new MultiSearchValidationException("Search terms are empty");
        }

        var availableSearchProviders = _serviceProvider.GetServices<ISearchProvider>();

        var result = new List<SearchResponse>();
        foreach (var searchProvider in availableSearchProviders)
        {
            var searchResult = new SearchResponse
            {
                Name = searchProvider.Name
            };
            
            _logger.LogInformation($"Executing search for {searchProvider.Name}");
            searchResult.HitsCountResult = PerformSearchAsync(searchProvider, searchTerms);

            result.Add(searchResult);
        }

        return result;
    }

    private async Task<IResult<int>?> PerformSearchAsync(ISearchProvider searchProvider, IEnumerable<string> searchTerms)
    {
        try
        {
            var searchTasks = searchTerms.Select(searchProvider.SearchAsync).ToList();
            var searchResultTask = Task.WhenAll(searchTasks);
            var searchResults = await searchResultTask;

            if (searchResults.Any(t => t.Success == false))
            {
                var errorResult = searchResults.First(t => t.Success == false);
                _logger.LogError($"{searchProvider.Name} returned en error, error - {errorResult.Message} ");
                
                return await Task.FromResult(errorResult);
            }

            return Result<int>.OK(searchResults.Sum(t => t.Data));
        }
        catch (Exception e)
        {
            _logger.LogError($"Exception occurs during search with {searchProvider.Name} ", e);

            return await Task.FromResult(Result<int>.Error($"Error occurs. Exception is - {e.Message}", -1));
        }
    }
}