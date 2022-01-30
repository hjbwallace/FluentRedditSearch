namespace FluentRedditSearch.Tests.Criteria
{
    public class AuthorFilterOutTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "+NOT+author";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithoutAuthors(properties);
        }
    }
}