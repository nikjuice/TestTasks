namespace MultiSearch.Models;

public class SearchResponse
{
    public string Name { get; set; }
    public Task<IResult<int>?> HitsCountResult { get; set; }
}