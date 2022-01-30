namespace FluentRedditSearch.Tests.Criteria
{
    public class SubredditFilterOutTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "+NOT+subreddit";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithoutSubreddits(properties);
        }
    }
}