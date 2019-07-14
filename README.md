# Fluent Reddit Search

Search reddit for content with a fluent syntax via the Reddit Search API

## Search Criteria

Create a query string using the fluent criteria notation. Call `Build` on completed criteria to return the query string that can be used against the Reddit Search API.

By default, the Reddit Search API will return at most 25 results unless another value is defined (between 1 and 100).


> Search for the highest rated posts from within the last year that are not marked as `Over18` in the `nfl` subreddit that contains `denver`. Return at most 10 entries with the highest scored post being first.

```c#
new RedditSearchCriteria()
	.WithTerm("denver")
    .WithSubreddits("nfl")
    .WithLimit(10)
    .WithOrdering(ResultOrdering.Top)
    .WithTimeFilter(ResultTimeFilter.Year)
    .WithoutOver18Results()
```

## Search Service

Use the built in search service to query the API using either a custom query string or the `RedditSearchCriteria` object. The service can either return the raw json that the Reddit Search API returns, or an array of the custom `RedditSearchResult` object.


```c#
var criteria = new RedditSearchCriteria()
	.WithTerm("Some search term")
	.WithLimit(100);
	
var service = new RedditSearchService();

// Return the raw json payload as a string
var payload = await service.GetPayloadAsync(criteria);

// Returns RedditSearchResult[]
var results = await service.GetResultsAsync(criteria);
```

There are also synchronous methods `GetResults` and `GetPayload` available as extensions upon the `IRedditSearchService` interface.

