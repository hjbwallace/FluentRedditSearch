namespace FluentRedditSearch.Tests.Criteria
{
    public class FlairFilterOutTests : QueryPropertyAllowSpacesTestBase
    {
        public override string PropertyName => "+NOT+flair";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithoutFlairs(properties);
        }
    }
}