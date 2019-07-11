using Newtonsoft.Json;

namespace FluentRedditSearch.SearchApi
{
    internal class Preview
    {
        [JsonProperty("images")]
        public Image[] Images { get; set; } = new Image[0];

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}