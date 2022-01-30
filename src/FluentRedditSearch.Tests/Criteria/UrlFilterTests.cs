namespace FluentRedditSearch.Tests.Criteria
{
    public class UrlFilterTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "url";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithUrls(properties);
        }
    }
}