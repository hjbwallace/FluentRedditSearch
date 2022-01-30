namespace FluentRedditSearch.Tests.Criteria
{
    public class SiteFilterTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "site";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithSites(properties);
        }
    }
}