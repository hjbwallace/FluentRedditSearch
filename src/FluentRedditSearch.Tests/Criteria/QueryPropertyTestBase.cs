using System;
using Xunit;

namespace FluentRedditSearch.Tests.Criteria
{
    public abstract class QueryPropertyNoSpacesTestBase : QueryPropertyTestBase
    {
        [Theory]
        [InlineData("Property Test")]
        [InlineData("Property Test Again")]
        public void ThrowsIfPropertyContainsSpace(string property)
        {
            var ex = Assert.Throws<ArgumentException>(() => BuildCriteria(property));
            Assert.Equal("No values can contain a space", ex.Message);
        }

        [Theory]
        [InlineData("Test1", "Test 2")]
        [InlineData("Another Test Again", "Test1")]
        public void ThrowsIfAnyPropertyContainsSpace(params string[] properties)
        {
            var ex = Assert.Throws<ArgumentException>(() => BuildCriteria(properties));
            Assert.Equal("No values can contain a space", ex.Message);
        }
    }

    public abstract class QueryPropertyAllowSpacesTestBase : QueryPropertyTestBase
    {
        [Theory]
        [InlineData("%22Property+Test%22", "Property Test")]
        [InlineData("%22Property+Test+Again%22", "Property Test Again")]
        public void CanFilterOnPropertyWithSpace(string expected, string property)
        {
            AssertQueryString($"search.json?q=++{PropertyName}%3A{expected}", property);
        }

        [Theory]
        [InlineData("(%22Test+1%22+OR+%22Test+2%22)", "Test 1", "Test 2")]
        [InlineData("(%22Test+1+2%22+OR+%22Test+3%22)", "Test 1 2", "Test 3")]
        public void CanFilterOnMultiplePropertyValuesWithSpace(string expected, params string[] properties)
        {
            AssertQueryString($"search.json?q=++{PropertyName}%3A{expected}", properties);
        }

        [Theory]
        [InlineData("(%22Test+1%22+OR+Test2)", "Test 1", "Test2")]
        [InlineData("(Test1+OR+%22Test+23%22+OR+Test)", "Test1", "Test 23", "Test")]
        public void CanFilterOnMultiplePropertyValuesWithAndWithoutSpace(string expected, params string[] properties)
        {
            AssertQueryString($"search.json?q=++{PropertyName}%3A{expected}", properties);
        }
    }

    public abstract class QueryPropertyTestBase
    {
        public abstract string PropertyName { get; }

        [Theory]
        [InlineData("Example", "Example")]
        [InlineData("Another", "     Another   ")]
        public void CanFilterOnProperty(string expected, string property)
        {
            AssertQueryString($"search.json?q=++{PropertyName}%3A{expected}", property);
        }

        [Theory]
        [InlineData("(Test1+OR+Test2+OR+Test3)", "Test1", "   Test2   ", "Test3")]
        [InlineData("(Example+OR+Another)", "   Example   ", "Another")]
        public void CanFilterOnMultiplePropertyValues(string expected, params string[] properties)
        {
            AssertQueryString($"search.json?q=++{PropertyName}%3A{expected}", properties);
        }

        [Theory]
        [InlineData("Test1", "Test1", "   Test1   ", "Test1  ")]
        [InlineData("(Example+OR+Another)", "   Example   ", "Another", "Another")]
        public void CanFilterOnMultiplePropertyValuesWithDuplicates(string expected, params string[] properties)
        {
            AssertQueryString($"search.json?q=++{PropertyName}%3A{expected}", properties);
        }

        [Theory]
        [InlineData("Test1", "Test1", "   ", "", null)]
        [InlineData("(Test1+OR+Test2)", "   ", "Test1", "", "Test2", null)]
        public void OnlyValidPropertyValuesAreFiltered(string expected, params string[] properties)
        {
            AssertQueryString($"search.json?q=++{PropertyName}%3A{expected}", properties);
        }

        [Theory]
        [InlineData("   ", "", null)]
        public void NoPropertiesFilteredOnWhenNoPopulatedPropertiesProvided(params string[] properties)
        {
            AssertQueryString($"search.json?q=", properties);
        }

        protected abstract RedditSearchCriteria BuildCriteria(params string[] properties);

        protected void AssertQueryString(string expected, params string[] properties)
        {
            var criteria = BuildCriteria(properties);
            var queryString = criteria.GetQueryString();
            Assert.Equal(expected, queryString);
        }
    }
}