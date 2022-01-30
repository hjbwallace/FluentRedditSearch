namespace FluentRedditSearch.Tests.Criteria
{
    public class TitleFilterOutTests : QueryPropertyAllowSpacesTestBase
    {
        public override string PropertyName => "+NOT+title";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithoutTitles(properties);
        }
    }
}