using MultiSearch.Models;

namespace MultiSearch.Services;

public interface ISearchProvider
{
    public Task<IResult<int>>  SearchAsync(string searchTerm);
    public string Name { get;}
}