using Newtonsoft.Json;
using System;

namespace FluentRedditSearch.SearchApi
{
    internal class ImageDetails
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }
}