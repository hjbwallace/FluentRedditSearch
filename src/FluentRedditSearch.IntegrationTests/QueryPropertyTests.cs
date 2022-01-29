using System;
using System.Linq;
using Xunit;

namespace FluentRedditSearch.IntegrationTests
{
    public class RedditSearchServiceQueryPropertyTests : IntegrationTestBase
    {
        [Theory]
        [InlineData("ScienceModerator")]
        public void SearchByAuthors(string author)
        {
            RunSearchTest(
                criteria => criteria.WithAuthors(author),
                should => should.OnlyContain(x => x.Author.Equals(author, StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [InlineData("ScienceModerator", "AutoModerator")]
        public void SearchByMultipleAuthors(params string[] authors)
        {
            RunSearchTest(
                criteria => criteria
                    .WithTerm("Test")
                    .WithAuthors(authors),
                should => should.OnlyContain(x => authors.Any(s => x.Author.Contains(s, StringComparison.InvariantCultureIgnoreCase)))
            );
        }

        [Theory]
        [InlineData("all")]
        public void SearchByFlairs(string flair)
        {
            RunSearchTest(
                criteria => criteria.WithFlairs(flair),
                should => should
                    .OnlyContain(x => !string.IsNullOrEmpty(x.Flair))
                    .And.OnlyContain(x => x.Flair.Contains(flair, StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [InlineData("music streaming", "video", "discussion")]
        [InlineData("Trailers", "News")]
        public void SearchByMultipleFlairs(params string[] flairs)
        {
            RunSearchTest(
                criteria => criteria.WithFlairs(flairs),
                should => should
                    .OnlyContain(x => !string.IsNullOrEmpty(x.Flair))
                    .And.OnlyContain(x => flairs.Any(s => x.Flair.Contains(s, StringComparison.InvariantCultureIgnoreCase)))
            );
        }

        [Theory]
        [InlineData("imgur")]
        [InlineData("reddit")]
        public void SearchBySite(string site)
        {
            RunSearchTest(
                criteria => criteria.WithSites(site),
                should => should
                    .OnlyContain(x => x.Domain.Contains(site, StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [InlineData("imgur", "gfycat")]
        [InlineData("reddit", "youtube")]
        public void SearchByMultipleSites(params string[] sites)
        {
            RunSearchTest(
                criteria => criteria
                    .WithTerm("Test")
                    .WithSites(sites),
                should => should.OnlyContain(x => sites.Any(s => x.Domain.Contains(s, StringComparison.InvariantCultureIgnoreCase)))
            );
        }

        [Theory]
        [InlineData("nfl")]
        [InlineData("games")]
        public void SearchBySubreddit(string subreddit)
        {
            RunSearchTest(
                criteria => criteria.WithSubreddits(subreddit),
                should => should.OnlyContain(x => x.Subreddit.Equals(subreddit, StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [InlineData("news", "worldnews")]
        [InlineData("games", "gaming")]
        [InlineData("music", "movies", "games")]
        public void SearchByMultipleSubreddits(params string[] subreddits)
        {
            RunSearchTest(
                criteria => criteria
                    .WithTerm("Test")
                    .WithSubreddits(subreddits),
                should => should.OnlyContain(x => subreddits.Any(s => x.Subreddit.Contains(s, StringComparison.InvariantCultureIgnoreCase)))
            );
        }

        [Fact]
        public void SearchForSelfPosts()
        {
            RunSearchTest(
                criteria => criteria.WithSelfPosts(),
                should => should.OnlyContain(x => x.IsSelfPost == true)
            );
        }

        [Fact]
        public void SearchForNonSelfPosts()
        {
            RunSearchTest(
                criteria => criteria.WithoutSelfPosts(),
                should => should.OnlyContain(x => x.IsSelfPost == false)
            );
        }

        [Theory]
        [InlineData("nfl")]
        [InlineData("game")]
        public void SearchByTitle(string title)
        {
            RunSearchTest(
                criteria => criteria.WithTitles(title),
                should => should.OnlyContain(x => x.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [InlineData("nfl")]
        [InlineData("game")]
        public void SearchBySelfText(string title)
        {
            RunSearchTest(
                criteria => criteria.WithTitles(title),
                should => should.OnlyContain(x => x.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [InlineData("nfl")]
        [InlineData("imgur")]
        [InlineData("gfycat")]
        public void SearchByUrl(string url)
        {
            RunSearchTest(
                criteria => criteria.WithUrls(url),
                should => should.OnlyContain(x => x.Url.AbsoluteUri.Contains(url, StringComparison.InvariantCultureIgnoreCase))
            );
        }
    }
}