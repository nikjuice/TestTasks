using MultiSearch.Models;
using IResult = MultiSearch.Models.IResult;

namespace MultiSearch.Services;

public interface ISearchManager
{
    public Task<IList<SearchResponse>> SearchAsync(IList<string> searchTerms);
}