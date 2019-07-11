using Newtonsoft.Json;

namespace FluentRedditSearch.SearchApi
{
    internal class SearchResponse
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("data")]
        public SearchResponseData Data { get; set; }
    }
}