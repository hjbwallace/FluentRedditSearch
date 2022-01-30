namespace FluentRedditSearch.Tests.Criteria
{
    public class FlairFilterTests : QueryPropertyAllowSpacesTestBase
    {
        public override string PropertyName => "flair";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithFlairs(properties);
        }
    }
}