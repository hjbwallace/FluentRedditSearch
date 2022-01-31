# Fluent Reddit Search

Search Reddit by creating complex queries with a fluent syntax.

Reddit has a public search API that allows searching posts by various criteria without going through the hassle of authenticating as a specific user. This API is accessed by hitting the `reddit.com/search.json?` endpoint with the criteria formatted in the query string.

## Prerequisites
* Install .NET 6

## RedditSearchCriteria

The `RedditSearchCriteria` class can be used to generate a query string that can be used against the Reddit search api.

Simply:
* Create a query string using the fluent criteria notation
* Use `GetQueryString` on the criteria to generate the query string
* Use the `RedditSearchService` or call the search API manually with the query

### With & Without

Properties can be included in the query with the `WithProperty` notation. Properties can also be specifically excluded from the query with the `WithoutProperty` notation. These can be used together or individually.

This allows you to do things like:
* Posts about a topic that are NOT in a certain subreddit
* Posts by everyone except a certain few authors
* Posts that don't have the 'Spoiler' flair

### Restrictions

The maximum number of results that can be returned from the API is 100
* The default is 25
* An exception will be thrown for an invalid value

Certain properties will allow spaces in a value. Those that don't will throw an exception when adding to the criteria.
Note: Allowing spaces is consistent when including or excluding properties from the criteria.

| Property | Allows Spaces |
| --- | --- |
| Author | No |
| Flair | Yes |
| Self Text | Yes |
| Site | No |
| Subreddit | No |
| Title | Yes |
| Url | No |

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

> Get the best 100 posts from the last week about the AFL withouth any posts from the `AFL` subreddit

```c#
new RedditSearchCriteria("AFL")
    .WithoutSubreddits("afl")
    .WithLimit(100)
    .WithOrdering(ResultOrdering.Top)
    .WithTimeFilter(ResultTimeFilter.Week)
```

## RedditSearchService

The `RedditSearchService` can be used to quickly get search results by providing a criteria object, or the raw query string. The service uses an internal parser to return `RedditSearchResult[]`.

### Usage

> Use the criteria directly in the search service

```c#
var criteria = new RedditSearchCriteria()
	.WithTerm("Some search term")
	.WithLimit(100);
	
var service = new RedditSearchService();
var results = await service.GetResultsAsync(criteria);
```

> Build the criteria via the service using the builder function

```c#
var results = await new RedditSearchService()
    .GetResultsAsync(criteria => criteria
        .WithTerm("Some search term")
	    .WithLimit(100));
```

> Search Reddit using a manual query string

```c#
var results = await new RedditSearchService()
    .GetResultsAsync("search.json?q=SearchTerm");
```

## Resources
* [Documentation for the Reddit search functionality](https://www.reddit.com/wiki/search)
* [Documentation for the Reddit API](https://www.reddit.com/dev/api#GET_search)