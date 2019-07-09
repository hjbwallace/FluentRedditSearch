using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public static class RedditSearchServiceExtensions
    {
        public static async Task<string> GetPayload(this IRedditSearchService client, RedditSearchCriteria criteria)
        {
            return await client.GetPayload(criteria.Build());
        }
    }
}