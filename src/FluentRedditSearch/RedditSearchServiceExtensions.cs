using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public static class RedditSearchServiceExtensions
    {
        public static async Task<RedditSearchResult[]> GetResultsAsync(this IRedditSearchService service, RedditSearchCriteria criteria)
        {
            return await service.GetResultsAsync(criteria.GetQueryString());
        }
    }
}