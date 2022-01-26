using FluentAssertions;
using FluentRedditSearch.Tests.Resources;
using FluentRedditSearch.Utilities;
using Newtonsoft.Json;
using System;
using System.Web;
using Xunit;

namespace FluentRedditSearch.Tests
{
    public class SearchResponseParserTests
    {
        [Fact]
        public void CanParsePayloads()
        {
            var payloads = ExamplePayloads.Get();

            foreach (var item in payloads)
            {
                var results = SearchResponseParser.Parse(item.Value.Search);
                var expectedResults = JsonConvert.DeserializeObject<RedditSearchResult[]>(item.Value.Results);

                try
                {
                    results.Should().BeEquivalentTo(expectedResults);
                }
                catch (Exception ex)
                {
                    throw new TestFailedException(item.Key, ex);
                }
            }
        }

        [Fact]
        public void CanParseExamplePayload()
        {
            var expected = new
            {
                Author = "_NITRISS_",
                CreatedAt = new DateTime(2019, 04, 15, 06, 47, 44, 0, DateTimeKind.Utc).ToLocalTime(),
                Domain = "i.imgur.com",
                Flair = "/r/ALL",
                Id = "bd8puw",
                IsSelfPost = false,
                IsOver18 = false,
                IsSpoiler = false,
                IsVideo = false,
                PostUrl = new Uri("https://www.reddit.com/r/interestingasfuck/comments/bd8puw/an_example_of_how_a_cameras_capture_rate_changes/"),
                Score = 114089,
                SelfText = (string)null,
                Subreddit = "interestingasfuck",
                Thumbnail = GetUriForThumbnail("https://external-preview.redd.it/P10uKC_pEPf74_cakYMPZHIzsKTbTr4H5fUmDufbW6c.jpg?width=960&amp;crop=smart&amp;auto=webp&amp;s=7acb8b5906ee935785cb5ca433099285add1b2c6"),
                Title = "An example of how a cameras capture rate changes due to the amount of light being let into the camera",
                Url = new Uri("https://i.imgur.com/2UdOULv.gifv"),
            };

            var results = SearchResponseParser.Parse(ExamplePayloads.Get("SingleResultExample").Search);

            results.Should().HaveCount(1);
            results[0].Should().BeEquivalentTo(expected);
        }

        private static Uri GetUriForThumbnail(string url)
        {
            return new Uri(HttpUtility.HtmlDecode(url));
        }
    }
}