namespace FluentRedditSearch.Tests.Criteria
{
    public class SubredditFilterTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "subreddit";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithSubreddits(properties);
        }
    }
}