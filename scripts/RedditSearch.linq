<Query Kind="Statements">
  <Reference Relative="..\src\FluentRedditSearch\bin\Debug\netstandard2.0\FluentRedditSearch.dll">FluentRedditSearch.dll</Reference>
  <Namespace>FluentRedditSearch</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

var criteria = new RedditSearchCriteria()
	.WithSubreddits("pics", "aww")
	.WithLimit(10)
	.WithoutSelfPosts()
	.WithOrdering(ResultOrdering.Top);
	
var queryString = criteria.GetQueryString();
//var queryString = "search.json?q=author%3A%28ScienceModerator+OR+AutoModerator%29&restrict_sr=";

queryString.Dump("Query String");
	
var service = new RedditSearchService();
var results = await service.GetResultsAsync(queryString);
results
	.Select(x => new { x.Id, x.Title, x.Subreddit, x.Score, x.SelfText, PostUrl = new Hyperlinq(x.PostUrl.AbsoluteUri), Url = new Hyperlinq(x.Url.AbsoluteUri), x.IsSelfPost })
	.Dump();