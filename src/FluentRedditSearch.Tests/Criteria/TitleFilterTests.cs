namespace FluentRedditSearch.Tests.Criteria
{
    public class TitleFilterTests : QueryPropertyAllowSpacesTestBase
    {
        public override string PropertyName => "title";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithTitles(properties);
        }
    }
}