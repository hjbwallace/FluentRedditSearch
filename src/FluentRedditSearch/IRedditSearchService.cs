using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public interface IRedditSearchService
    {
        Task<RedditSearchResult[]> GetResultsAsync(string queryString);
    }
}