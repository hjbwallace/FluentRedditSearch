using FluentRedditSearch.SearchApi;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;

namespace FluentRedditSearch
{
    public static class SearchResponseParser
    {
        private static readonly string _redditBaseAddress = @"https://www.reddit.com";

        public static RedditSearchResult[] Parse(string json)
        {
            var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(json);
            var childDatas = searchResponse.Data.Children.Select(x => x.Data);

            return childDatas.Select(MapToSearchResult).ToArray();
        }

        private static RedditSearchResult MapToSearchResult(ChildData data)
        {
            return new RedditSearchResult
            {
                Id = data.Id,
                Title = data.Title,
                SelfText = data.SelfText,
                Url = data.Url,
                Domain = data.Domain,
                Score = data.Score,
                Flair = data.LinkFlairText,
                CreatedAt = UnixTimeStampToDateTime(data.Created),
                Author = data.Author,
                Subreddit = data.Subreddit,
                PostUrl = new Uri(_redditBaseAddress + data.Permalink),
                Thumbnail = GetThumbnailFromPreview(data),
                IsOver18 = data.Over18,
                IsSelfPost = data.IsSelf,
                IsSpoiler = data.Spoiler,
                IsVideo = data.IsVideo
            };
        }

        private static string GetThumbnailFromPreview(ChildData data)
        {
            var absoluteUri = data?.Preview?
                .Images
                .FirstOrDefault()?
                .Resolutions
                .OrderByDescending(r => r.Height * r.Width)
                .FirstOrDefault()?
                .Url
                .AbsoluteUri;

            if (absoluteUri == null)
                return null;

            return HttpUtility.HtmlDecode(absoluteUri);
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var utcRootTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return utcRootTime.AddSeconds(unixTimeStamp).ToLocalTime();
        }
    }
}