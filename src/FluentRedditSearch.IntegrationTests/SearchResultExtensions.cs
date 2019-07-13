using FluentAssertions;
using System;
using System.Linq;

namespace FluentRedditSearch.IntegrationTests
{
    internal static class SearchResultExtensions
    {
        public static RedditSearchResult[] AssertResultOrdering<TResult>(this RedditSearchResult[] results, Func<RedditSearchResult, TResult> selector)
        {
            var expected = results.Max(selector);
            var orderedProperty = selector(results[0]);
            orderedProperty.Should().Be(expected);
            return results;
        }
    }
}