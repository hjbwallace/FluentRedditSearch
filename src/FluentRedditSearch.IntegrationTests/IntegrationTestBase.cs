﻿using FluentAssertions;
using FluentAssertions.Collections;
using System;

namespace FluentRedditSearch.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        private readonly IRedditSearchService _redditSearchService = new RedditSearchService();

        protected void RunSearchTest(
            Func<RedditSearchCriteria, RedditSearchCriteria> criteriaFunc,
            Action<GenericCollectionAssertions<RedditSearchResult>> assertAction = null,
            Action<RedditSearchResult[]> additionalResultsAction = null)
        {
            var results = _redditSearchService.GetResultsAsync(criteriaFunc).GetAwaiter().GetResult();

            assertAction?.Invoke(results.Should());

            additionalResultsAction?.Invoke(results);
        }

        protected void RunSearchTest(
            Func<RedditSearchCriteria, RedditSearchCriteria> criteriaFunc,
            Action<RedditSearchResult[]> additionalResultsAction = null)
        {
            RunSearchTest(criteriaFunc, null, additionalResultsAction);
        }
    }
}