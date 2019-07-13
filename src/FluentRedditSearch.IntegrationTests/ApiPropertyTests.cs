using Xunit;

namespace FluentRedditSearch.IntegrationTests
{
    public class RedditSearchServiceApiPropertyTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        public void SearchWithLimit(int limit)
        {
            RunSearchTest(
                criteria => criteria.WithTerm("test").WithLimit(limit),
                should => should.HaveCount(limit)
            );
        }

        [Fact]
        public void SearchWithOver18()
        {
            RunSearchTest(
                criteria => criteria.WithTerm("test").WithoutOver18Results(),
                should => should.OnlyContain(x => x.IsOver18 == false)
            );
        }

        [Fact]
        public void SearchWithScoreOrdering()
        {
            RunSearchTest(
                criteria => criteria.WithTerm("test").WithOrdering(ResultOrdering.Top),
                results => results.AssertResultOrdering(x => x.Score)
            );
        }

        [Fact]
        public void SearchWithDateOrdering()
        {
            RunSearchTest(
                criteria => criteria.WithTerm("test").WithOrdering(ResultOrdering.New),
                results => results.AssertResultOrdering(x => x.CreatedAt)
            );
        }
    }
}