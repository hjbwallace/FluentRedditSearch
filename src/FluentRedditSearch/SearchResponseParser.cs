using FluentRedditSearch.SearchApi;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

[assembly: InternalsVisibleTo("FluentRedditSearch.Tests")]

namespace FluentRedditSearch
{
    internal static class SearchResponseParser
    {
        private static readonly string RedditBaseAddress = @"https://www.reddit.com";

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
                Title = FormatText(data.Title),
                SelfText = FormatText(data.SelfText),
                Url = data.Url,
                Domain = FormatText(data.Domain),
                Score = data.Score,
                Flair = FormatText(data.LinkFlairText),
                CreatedAt = UnixTimeStampToDateTime(data.Created),
                Author = data.Author,
                Subreddit = data.Subreddit,
                PostUrl = new Uri(RedditBaseAddress + data.Permalink),
                Thumbnail = GetThumbnailFromPreview(data),
                IsOver18 = data.Over18,
                IsSelfPost = data.IsSelf,
                IsSpoiler = data.Spoiler,
                IsVideo = data.IsVideo
            };
        }

        private static string FormatText(string source)
        {
            return string.IsNullOrWhiteSpace(source) ? null : source.Trim();
        }

        private static Uri GetThumbnailFromPreview(ChildData data)
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

            var decoded = HttpUtility.HtmlDecode(absoluteUri);
            return new Uri(decoded);
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var utcRootTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return utcRootTime.AddSeconds(unixTimeStamp).ToLocalTime();
        }
    }
}