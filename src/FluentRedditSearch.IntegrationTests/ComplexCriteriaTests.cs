using System;
using Xunit;

namespace FluentRedditSearch.IntegrationTests
{
    public class ComplexCriteriaTests : IntegrationTestBase
    {
        [Fact]
        public void SearchWithMultipleProperties()
        {
            RunSearchTest(
                criteria => criteria
                    .WithTerm("denver")
                    .WithSubreddits("nfl")
                    .WithLimit(10)
                    .WithOrdering(ResultOrdering.Top)
                    .WithTimeFilter(ResultTimeFilter.Year)
                    .WithoutOver18Results(),
                should => should
                    .HaveCount(10)
                    .And.OnlyContain(x => x.Subreddit == "nfl")
                    .And.OnlyContain(x => x.IsOver18 == false)
                    .And.OnlyContain(x => x.CreatedAt > DateTime.Now.AddDays(-367)),
                results => results
                    .AssertResultOrdering(x => x.Score)
            );
        }
    }
}