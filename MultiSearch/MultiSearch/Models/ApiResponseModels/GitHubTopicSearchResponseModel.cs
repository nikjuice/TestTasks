using System.Text.Json.Serialization;

namespace MultiSearch.Models.ApiResponseModels;

public class GitHubTopicSearchResponseModel
{
    [JsonPropertyName("total_count")]
    public int TotalHits { get; set; }
}
