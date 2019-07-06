using Xunit;

namespace FluentRedditSearch.Tests
{
    public class SearchCriteriaQueryStringTests
    {
        private readonly RedditSearchCriteria _criteria = new RedditSearchCriteria();

        [Fact]
        public void CanGenerate()
        {
            var expected = "search.json?q=Search+term";

            var queryString = new RedditSearchCriteria("Search term").Build();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanGenerateForWithMultipleSitesAndFlairs()
        {
            var expected = "search.json?q=Search+term+site%3ASite1+OR+Site2+flair%3AFlair1+OR+Flair2&limit=99&sort=top";

            var queryString = _criteria
                .WithLimit(99)
                .WithTerm("Search term")
                .WithSites("Site1", "Site2")
                .WithFlairs("Flair1", "Flair2")
                .WithOrdering(ResultOrdering.Top)
                .Build();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanGenerateWithAllParameters()
        {
            var expected = "search.json?q=Some+search+term+author%3AAuthor1+OR+Author2+flair%3AFlair1+OR+Flair2+site%3ASite1+OR+Site2+subreddit%3ASubreddit1+OR+Subreddit2&limit=99&sort=comments&include_over_18=on&t=month";

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
                .Build();

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
                .Build();

            Assert.Equal(expected, queryString);
        }
    }
}