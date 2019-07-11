using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public static class RedditSearchServiceExtensions
    {
        public static async Task<string> GetPayload(this IRedditSearchService client, RedditSearchCriteria criteria)
        {
            return await client.GetPayload(criteria.Build());
        }

        public static async Task<RedditSearchResult[]> GetResults(this IRedditSearchService client, RedditSearchCriteria criteria)
        {
            return await client.GetResults(criteria.Build());
        }

        public static async Task<RedditSearchResult[]> GetResults(this IRedditSearchService client, string queryString)
        {
            var payload = await client.GetPayload(queryString);
            return SearchResponseParser.Parse(payload);
        }
    }
}