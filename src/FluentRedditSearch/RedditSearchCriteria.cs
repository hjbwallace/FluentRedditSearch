using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentRedditSearch
{
    public class RedditSearchCriteria
    {
        private readonly IDictionary<string, object> _apiProperties = new Dictionary<string, object>();
        private readonly IDictionary<string, object[]> _queryProperties = new Dictionary<string, object[]>();
        private string _searchTerm;

        public RedditSearchCriteria(string searchTerm)
            : this()
        {
            WithTerm(searchTerm);
        }

        public RedditSearchCriteria()
        {
            WithLimit(100);
        }

        public string Build()
        {
            var sb = new StringBuilder($"search.json?q={_searchTerm}");

            foreach (var queryProperty in _queryProperties)
            {
                sb.Append(" ");
                sb.Append(queryProperty.Key + "%3A");
                sb.Append(string.Join(" OR ", queryProperty.Value));
            }

            foreach (var apiProperty in _apiProperties)
                sb.Append($"&{apiProperty.Key}={apiProperty.Value}");

            return sb.ToString().Replace(' ', '+');
        }

        public RedditSearchCriteria WithAuthors(params string[] authors)
        {
            WithQueryProperty("author", authors);
            return this;
        }

        public RedditSearchCriteria WithFlairs(params string[] flairs)
        {
            WithQueryProperty("flair", flairs);
            return this;
        }

        public RedditSearchCriteria WithLimit(int limit)
        {
            if (limit <= 0 || limit > 100)
                throw new ArgumentException("Limit is not within the accepted range of 1 to 100");

            return WithApiProperty("limit", limit);
        }

        public RedditSearchCriteria WithOrdering(ResultOrdering ordering)
        {
            return WithApiProperty("sort", ordering.ToString().ToLower());
        }

        public RedditSearchCriteria WithoutOver18Results() => WithOver18(false);

        public RedditSearchCriteria WithOver18Results() => WithOver18(true);

        public RedditSearchCriteria WithSites(params string[] sites)
        {
            return WithQueryProperty("site", sites);
        }

        public RedditSearchCriteria WithSubreddits(params string[] subreddits)
        {
            return WithQueryProperty("subreddit", subreddits);
        }

        public RedditSearchCriteria WithTerm(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term must be populated");

            _searchTerm = searchTerm;
            return this;
        }

        public RedditSearchCriteria WithTimeFilter(ResultTimeFilter timeSpan)
        {
            return WithApiProperty("t", timeSpan.ToString().ToLower());
        }

        private RedditSearchCriteria WithApiProperty(string property, object value)
        {
            if (string.IsNullOrEmpty(property))
                throw new ArgumentException("Property must be populated");

            if (string.IsNullOrEmpty(value?.ToString()))
                throw new ArgumentException("Value must be populated");

            _apiProperties[property] = value.ToString();
            return this;
        }

        private RedditSearchCriteria WithOver18(bool includeOver18)
        {
            return WithApiProperty("include_over_18", includeOver18 ? "on" : "off");
        }

        private RedditSearchCriteria WithQueryProperty(string property, string[] values)
        {
            if (values?.Any() != true)
                throw new ArgumentException("Values must be populated");

            _queryProperties[property] = values;
            return this;
        }
    }
}