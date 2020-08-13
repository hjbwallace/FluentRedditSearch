using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public class RedditSearchService : IRedditSearchService, IDisposable
    {
        private readonly HttpClient _client;

        public RedditSearchService()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://www.reddit.com/") };
        }

        public void Dispose() => _client.Dispose();

        public async Task<RedditSearchResult[]> GetResultsAsync(string queryString)
        {
            var payload = await GetPayloadAsync(queryString);
            return SearchResponseParser.Parse(payload);
        }

        private async Task<string> GetPayloadAsync(string queryString)
        {
            var response = await _client.GetAsync(queryString);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}