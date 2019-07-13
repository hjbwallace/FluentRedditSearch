using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public static class RedditSearchServiceExtensions
    {
        public static async Task<string> GetPayloadAsync(this IRedditSearchService service, RedditSearchCriteria criteria)
        {
            return await service.GetPayloadAsync(criteria.Build());
        }

        public static string GetPayload(this IRedditSearchService service, string queryString)
        {
            return service.GetPayloadAsync(queryString).Result;
        }

        public static string GetPayload(this IRedditSearchService service, RedditSearchCriteria criteria)
        {
            return service.GetPayloadAsync(criteria).Result;
        }

        public static async Task<RedditSearchResult[]> GetResultsAsync(this IRedditSearchService service, RedditSearchCriteria criteria)
        {
            return await service.GetResultsAsync(criteria.Build());
        }

        public static async Task<RedditSearchResult[]> GetResultsAsync(this IRedditSearchService service, string queryString)
        {
            var payload = await service.GetPayloadAsync(queryString);
            return SearchResponseParser.Parse(payload);
        }

        public static RedditSearchResult[] GetResults(this IRedditSearchService service, RedditSearchCriteria criteria)
        {
            return service.GetResultsAsync(criteria).Result;
        }

        public static RedditSearchResult[] GetResults(this IRedditSearchService service, string queryString)
        {
            return service.GetResultsAsync(queryString).Result;
        }
    }
}