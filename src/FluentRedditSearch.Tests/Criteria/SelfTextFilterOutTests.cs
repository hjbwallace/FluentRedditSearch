namespace FluentRedditSearch.Tests.Criteria
{
    public class SelfTextFilterOutTests : QueryPropertyAllowSpacesTestBase
    {
        public override string PropertyName => "+NOT+selftext";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithoutSelfText(properties);
        }
    }
}