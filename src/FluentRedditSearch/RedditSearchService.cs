using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public class RedditSearchService : IRedditSearchService
    {
        public async Task<string> GetPayload(string queryString)
        {
            var client = new HttpClient { BaseAddress = new Uri("https://www.reddit.com/") };

            var response = await client.GetAsync(queryString);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}