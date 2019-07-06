using System;
using Xunit;

namespace FluentRedditSearch.Tests
{
    public class SearchCriteriaErrorHandlingTests
    {
        private readonly RedditSearchCriteria _criteria = new RedditSearchCriteria();

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        public void ThrowsForInvalidTerm(string term)
        {
            Action action = () => _criteria.WithTerm(term);
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfAnyAuthorIsInvalid()
        {
            Action action = () => _criteria.WithAuthors("ValidAuthor", "AnotherAuthor", "");
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfAnyFlairIsInvalid()
        {
            Action action = () => _criteria.WithFlairs("Valid Flair", "Another Flair", "");
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfAnySiteIsInvalid()
        {
            Action action = () => _criteria.WithSites("Site1", "Site2", "");
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfAnySubredditIsInvalid()
        {
            Action action = () => _criteria.WithSubreddits("Subreddit", "Subreddit2", "");
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("With Space")]
        public void ThrowsIfAuthorIsInvalid(string author)
        {
            Action action = () => _criteria.WithAuthors(author);
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        public void ThrowsIfFlairIsInvalid(string flair)
        {
            Action action = () => _criteria.WithFlairs(flair);
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(-10)]
        public void ThrowsIfLimitOutOfBounds(int limit)
        {
            Action action = () => _criteria.WithLimit(limit);
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfNoAuthorsProvided()
        {
            Action action = () => _criteria.WithAuthors();
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfNoFlairsProvided()
        {
            Action action = () => _criteria.WithFlairs();
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfNoSiteProvided()
        {
            Action action = () => _criteria.WithSites();
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ThrowsIfNoSubredditsProvided()
        {
            Action action = () => _criteria.WithSubreddits();
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("With Space")]
        public void ThrowsIfSiteIsInvalid(string site)
        {
            Action action = () => _criteria.WithSites(site);
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("With Space")]
        public void ThrowsIfSubredditIsInvalid(string subreddit)
        {
            Action action = () => _criteria.WithSubreddits(subreddit);
            Assert.Throws<ArgumentException>(action);
        }
    }
}