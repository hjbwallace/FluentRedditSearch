using System;
using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public static class RedditSearchServiceExtensions
    {
        public static async Task<RedditSearchResult[]> GetResultsAsync(this IRedditSearchService service, RedditSearchCriteria criteria)
        {
            return await service.GetResultsAsync(criteria.GetQueryString());
        }

        public static async Task<RedditSearchResult[]> GetResultsAsync(this IRedditSearchService service, Func<RedditSearchCriteria, RedditSearchCriteria> criteriaFunc)
        {
            var criteria = criteriaFunc(new RedditSearchCriteria());
            return await service.GetResultsAsync(criteria);
        }
    }
}