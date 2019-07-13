using System;
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
        [InlineData("nfl")]
        [InlineData("games")]
        public void SearchBySubreddit(string subreddit)
        {
            RunSearchTest(
                criteria => criteria.WithSubreddits(subreddit),
                should => should.OnlyContain(x => x.Subreddit.Equals(subreddit, StringComparison.InvariantCultureIgnoreCase))
            );
        }
    }
}