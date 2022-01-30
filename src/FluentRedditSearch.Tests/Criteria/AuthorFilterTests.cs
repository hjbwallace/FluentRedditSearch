namespace FluentRedditSearch.Tests.Criteria
{
    public class AuthorFilterTests : QueryPropertyNoSpacesTestBase
    {
        public override string PropertyName => "author";

        protected override RedditSearchCriteria BuildCriteria(params string[] properties)
        {
            return new RedditSearchCriteria().WithAuthors(properties);
        }
    }
}