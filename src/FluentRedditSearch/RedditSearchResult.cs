using System;

namespace FluentRedditSearch
{
    public class RedditSearchResult
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Domain { get; set; }
        public string Flair { get; set; }        
        public bool IsSelfPost { get; set; }
        public bool IsVideo { get; set; }
        public bool IsSpoiler { get; set; }
        public bool IsOver18 { get; set; }
        public Uri PostUrl { get; set; }
        public int Score { get; set; }
        public string SelfText { get; set; }
        public string Subreddit { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
    }
}