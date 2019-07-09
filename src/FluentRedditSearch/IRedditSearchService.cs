using System.Threading.Tasks;

namespace FluentRedditSearch
{
    public interface IRedditSearchService
    {
        Task<string> GetPayload(string queryString);
    }
}