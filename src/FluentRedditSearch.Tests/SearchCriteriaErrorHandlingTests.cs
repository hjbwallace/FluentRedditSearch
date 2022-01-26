using System;
using Xunit;

namespace FluentRedditSearch.Tests
{
    public class SearchCriteriaErrorHandlingTests
    {
        private readonly RedditSearchCriteria _criteria = new();

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        public void ThrowsForInvalidTerm(string term)
        {
            void BuildCriteria() => _criteria.WithTerm(term);
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfAnyAuthorIsInvalid()
        {
            void BuildCriteria() => _criteria.WithAuthors("ValidAuthor", "AnotherAuthor", "");
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfAnyFlairIsInvalid()
        {
            void BuildCriteria() => _criteria.WithFlairs("Valid Flair", "Another Flair", "");
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfAnySiteIsInvalid()
        {
            void BuildCriteria() => _criteria.WithSites("Site1", "Site2", "");
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfAnySubredditIsInvalid()
        {
            void BuildCriteria() => _criteria.WithSubreddits("Subreddit", "Subreddit2", "");
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("With Space")]
        public void ThrowsIfAuthorIsInvalid(string author)
        {
            void BuildCriteria() => _criteria.WithAuthors(author);
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        public void ThrowsIfFlairIsInvalid(string flair)
        {
            void BuildCriteria() => _criteria.WithFlairs(flair);
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(-10)]
        public void ThrowsIfLimitOutOfBounds(int limit)
        {
            void BuildCriteria() => _criteria.WithLimit(limit);
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfNoAuthorsProvided()
        {
            void BuildCriteria() => _criteria.WithAuthors();
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfNoFlairsProvided()
        {
            void BuildCriteria() => _criteria.WithFlairs();
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfNoSiteProvided()
        {
            void BuildCriteria() => _criteria.WithSites();
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Fact]
        public void ThrowsIfNoSubredditsProvided()
        {
            void BuildCriteria() => _criteria.WithSubreddits();
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("With Space")]
        public void ThrowsIfSiteIsInvalid(string site)
        {
            void BuildCriteria() => _criteria.WithSites(site);
            Assert.Throws<ArgumentException>(BuildCriteria);
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("With Space")]
        public void ThrowsIfSubredditIsInvalid(string subreddit)
        {
            void BuildCriteria() => _criteria.WithSubreddits(subreddit);
            Assert.Throws<ArgumentException>(BuildCriteria);
        }
    }
}