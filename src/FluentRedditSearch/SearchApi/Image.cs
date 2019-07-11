using Newtonsoft.Json;

namespace FluentRedditSearch.SearchApi
{
    internal class Image
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resolutions")]
        public ImageDetails[] Resolutions { get; set; } = new ImageDetails[0];

        [JsonProperty("source")]
        public ImageDetails Source { get; set; }
    }
}