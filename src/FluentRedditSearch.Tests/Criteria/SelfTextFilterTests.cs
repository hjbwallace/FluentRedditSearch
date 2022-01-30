namespace FluentRedditSearch.Tests.Criteria
{
    public class SelfTextFilterTests : QueryPropertyAllowSpacesTestBase
    {
        public override string PropertyName => "selftext";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithSelfText(properties);
        }
    }
}