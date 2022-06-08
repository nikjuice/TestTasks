using System.Text.Json.Serialization;

namespace MultiSearch.Models.ApiResponseModels;

public class WikipediaSearchResponseModel
{
    [JsonPropertyName("query")]
    public Query Query { get; set; }
}

public class Query
{ 
    [JsonPropertyName("searchinfo")]
    public SearchInfo SearchInfo { get; set; }
}

public class SearchInfo
{ 
    [JsonPropertyName("totalhits")]
    public int TotalHits { get; set; }
}