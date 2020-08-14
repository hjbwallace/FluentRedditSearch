# Fluent Reddit Search

Search Reddit by creating complex queries with a fluent syntax.

Reddit has a public search API that allows searching posts by various criteria without going through the hassle of authenticating as a specific user. This API is accessed by hitting the `reddit.com/search.json?` endpoint with the criteria formatted in the query string.

## RedditSearchCriteria

The `RedditSearchCriteria` object can be used to generate the query string 

Create a query string using the fluent criteria notation. Call `Build` on completed criteria to return the query string that can be used against the Reddit Search API.

### Restrictions

* Flairs is the only query property that allows spaces in the provided values. Other properties (Authors, Sites, Subreddits) will throw an exception
* The API will return 25 results by default. Values between 1 and 100 can be provided via the criteria

### Usage

> Get the highest rated posts about `denver` from within the last year that are not marked as `Over18` in the `nfl` subreddit. Only the top 10 results should be returned with the highest rated being the first returned.

```c#
new RedditSearchCriteria()
	.WithTerm("denver")
    .WithSubreddits("nfl")
    .WithLimit(10)
    .WithOrdering(ResultOrdering.Top)
    .WithTimeFilter(ResultTimeFilter.Year)
    .WithoutOver18Results()
```

> Get the best 50 cat posts of all time from either Imgur or Gfycat

```c#
new RedditSearchCriteria("cat")
    .WithSites("imgur", "gfycat")
    .WithLimit(50)
    .WithOrdering(ResultOrdering.Top)
    .WithTimeFilter(ResultTimeFilter.All)
```

## RedditSearchService

The `RedditSearchService` can be used to quickly get search results by providing a criteria object, or the raw query string. The service uses an internal parser to return `RedditSearchResult[]`.

### Usage

```c#
var criteria = new RedditSearchCriteria()
	.WithTerm("Some search term")
	.WithLimit(100);
	
var service = new RedditSearchService();
var results = await service.GetResultsAsync(criteria);
```

```c#
var results = await new RedditSearchService()
    .GetResultsAsync(criteria => criteria
        .WithTerm("Some search term")
	    .WithLimit(100));
```

```c#
var results = await new RedditSearchService()
    .GetResultsAsync("search.json?q=Search+term");
```

## Resources
* [Documentation for the Reddit search functionality](https://www.reddit.com/wiki/search)
* [Documentation for the Reddit API](https://www.reddit.com/dev/api#GET_search)