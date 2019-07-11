using Newtonsoft.Json;
using System;

namespace FluentRedditSearch.SearchApi
{
    internal class ChildData
    {
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_self")]
        public bool IsSelf { get; set; }

        [JsonProperty("is_video")]
        public bool IsVideo { get; set; }

        [JsonProperty("link_flair_text")]
        public string LinkFlairText { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("preview")]
        public Preview Preview { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("selftext")]
        public string SelfText { get; set; }

        [JsonProperty("spoiler")]
        public bool Spoiler { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}