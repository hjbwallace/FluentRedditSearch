using Newtonsoft.Json;

namespace FluentRedditSearch.SearchApi
{
    internal class SearchResponseData
    {
        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Before { get; set; }

        [JsonProperty("children")]
        public Child[] Children { get; set; } = new Child[0];

        [JsonProperty("dist")]
        public long Dist { get; set; }

        [JsonProperty("modhash")]
        public string Modhash { get; set; }
    }
}