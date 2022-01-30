namespace FluentRedditSearch.Tests.Criteria
{
    public class SiteFilterOutTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "+NOT+site";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithoutSites(properties);
        }
    }
}