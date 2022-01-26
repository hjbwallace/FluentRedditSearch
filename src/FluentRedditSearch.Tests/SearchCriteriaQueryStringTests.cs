using Xunit;

namespace FluentRedditSearch.Tests
{
    public class SearchCriteriaQueryStringTests
    {
        private readonly RedditSearchCriteria _criteria = new();

        [Fact]
        public void CanGenerate()
        {
            var expected = "search.json?q=Search+term";

            var queryString = new RedditSearchCriteria("Search term").GetQueryString();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanGenerateForWithMultipleSitesAndFlairs()
        {
            var expected = "search.json?q=Search+term+site%3A(Site1+OR+Site2)+flair%3A(Flair1+OR+Flair2)&limit=99&sort=top";

            var queryString = _criteria
                .WithLimit(99)
                .WithTerm("Search term")
                .WithSites("Site1", "Site2")
                .WithFlairs("Flair1", "Flair2")
                .WithOrdering(ResultOrdering.Top)
                .GetQueryString();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanGenerateWithAllParameters()
        {
            var expected = "search.json?q=Some+search+term+author%3A(Author1+OR+Author2)+flair%3A(Flair1+OR+Flair2)+site%3A(Site1+OR+Site2)+subreddit%3A(Subreddit1+OR+Subreddit2)&limit=99&sort=comments&include_over_18=on&t=month";

            var queryString = _criteria
                .WithAuthors("Author1", "Author2")
                .WithFlairs("Flair1", "Flair2")
                .WithLimit(99)
                .WithOrdering(ResultOrdering.Comments)
                .WithOver18Results()
                .WithSites("Site1", "Site2")
                .WithSubreddits("Subreddit1", "Subreddit2")
                .WithTerm("Some search term")
                .WithTimeFilter(ResultTimeFilter.Month)
                .GetQueryString();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanGenerateWithParameters()
        {
            var expected = "search.json?q=Search+term+subreddit%3ASubreddit&limit=25&include_over_18=off";

            var queryString = _criteria
                .WithLimit(25)
                .WithSubreddits("Subreddit")
                .WithTerm("Search term")
                .WithoutOver18Results()
                .GetQueryString();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void MostRecentPropertyIsUsedInQueryString()
        {
            var expected = "search.json?q=Otherterm+subreddit%3ASubredditNew&limit=5&include_over_18=on";

            var queryString = _criteria
                .WithLimit(25)
                .WithLimit(5)
                .WithSubreddits("Subreddit")
                .WithSubreddits("SubredditNew")
                .WithTerm("Search term")
                .WithTerm("Otherterm")
                .WithoutOver18Results()
                .WithOver18Results()
                .GetQueryString();

            Assert.Equal(expected, queryString);
        }
    }
}