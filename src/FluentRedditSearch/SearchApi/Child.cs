using Newtonsoft.Json;

namespace FluentRedditSearch.SearchApi
{
    internal class Child
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("data")]
        public ChildData Data { get; set; }
    }
}