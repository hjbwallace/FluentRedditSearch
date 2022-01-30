namespace FluentRedditSearch.Tests.Criteria
{
    public class UrlFilterOutTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "+NOT+url";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithoutUrls(properties);
        }
    }
}