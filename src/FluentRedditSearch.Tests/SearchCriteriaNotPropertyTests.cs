using Xunit;

namespace FluentRedditSearch.Tests
{
    public class SearchCriteriaNotPropertyTests
    {
        private readonly RedditSearchCriteria _criteria = new("Test");

        [Fact]
        public void CanExcludeSubredditsFromQueryString()
        {
            var expected = "search.json?q=Test++NOT+subreddit%3ASubredditExcluded";
            var queryString = _criteria.WithoutSubreddits("SubredditExcluded").GetQueryString();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanExcludeAuthorsFromQueryString()
        {
            var expected = "search.json?q=Test++NOT+author%3AAuthorExcluded";
            var queryString = _criteria.WithoutAuthors("AuthorExcluded").GetQueryString();

            Assert.Equal(expected, queryString);
        }

        [Fact]
        public void CanExcludeFlairsFromQueryString()
        {
            var expected = "search.json?q=Test++NOT+flair%3AFlairExcluded";
            var queryString = _criteria.WithoutFlairs("FlairExcluded").GetQueryString();

            Assert.Equal(expected, queryString);
        }
    }
}