using FluentRedditSearch.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace FluentRedditSearch
{
    public class RedditSearchCriteria
    {
        private readonly IDictionary<string, string> _apiProperties = new Dictionary<string, string>();
        private readonly IDictionary<string, string[]> _queryProperties = new Dictionary<string, string[]>();
        private readonly IDictionary<string, string[]> _queryNotProperties = new Dictionary<string, string[]>();
        private string _searchTerm;

        public RedditSearchCriteria(string searchTerm)
            : this()
        {
            WithTerm(searchTerm);
        }

        public RedditSearchCriteria()
        {
        }

        public string GetQueryString()
        {
            var sb = new StringBuilder($"search.json?q={(_searchTerm == null ? " " : WebUtility.UrlEncode(_searchTerm))} ");

            if (_queryProperties.Any())
                sb.Append(QueryStringHelper.GetQueryProperties(_queryProperties));

            if (_queryNotProperties.Any())
                sb.Append(" NOT " + QueryStringHelper.GetQueryProperties(_queryNotProperties));

            if (_apiProperties.Any())
                sb.Append(QueryStringHelper.GetApiProperties(_apiProperties));

            return sb.ToString().Trim().Replace(" ", "+");
        }

        public RedditSearchCriteria WithAuthors(params string[] authors)
        {
            WithQueryProperty("author", authors, false);
            return this;
        }

        public RedditSearchCriteria WithoutAuthors(params string[] authors)
        {
            WithoutQueryProperty("author", authors, false);
            return this;
        }

        public RedditSearchCriteria WithFlairs(params string[] flairs)
        {
            WithQueryProperty("flair", flairs, true);
            return this;
        }

        public RedditSearchCriteria WithoutFlairs(params string[] flairs)
        {
            WithoutQueryProperty("flair", flairs, true);
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
            return WithQueryProperty("site", sites, false);
        }

        public RedditSearchCriteria WithSubreddits(params string[] subreddits)
        {
            return WithQueryProperty("subreddit", subreddits, false);
        }

        public RedditSearchCriteria WithoutSubreddits(params string[] subreddits)
        {
            return WithoutQueryProperty("subreddit", subreddits, false);
        }

        public RedditSearchCriteria WithUrls(params string[] urls)
        {
            return WithQueryProperty("url", urls, false);
        }

        public RedditSearchCriteria WithoutUrls(params string[] urls)
        {
            return WithoutQueryProperty("url", urls, false);
        }

        public RedditSearchCriteria WithTitles(params string[] title)
        {
            return WithQueryProperty("title", title, true);
        }

        public RedditSearchCriteria WithoutTitles(params string[] titles)
        {
            return WithoutQueryProperty("title", titles, true);
        }

        public RedditSearchCriteria WithSelfText(params string[] text)
        {
            return WithQueryProperty("selftext", text, true);
        }

        public RedditSearchCriteria WithoutSelfText(params string[] text)
        {
            return WithoutQueryProperty("selftext", text, true);
        }

        public RedditSearchCriteria WithTerm(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term must be populated");

            _searchTerm = FormatString(searchTerm);
            return this;
        }

        public RedditSearchCriteria WithSelfPosts() => WithSelfPosts(true);

        public RedditSearchCriteria WithoutSelfPosts() => WithSelfPosts(false);

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

        private RedditSearchCriteria WithSelfPosts(bool includeSelfPosts)
        {
            return WithQueryProperty("self", new[] { includeSelfPosts ? "1" : "0" }, false);
        }

        private RedditSearchCriteria WithQueryProperty(string property, string[] values, bool allowSpaces)
        {
            if (values?.Any() != true)
                throw new ArgumentException("Values must be populated");

            if (values.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("All values must be populated");

            if (!allowSpaces && values.Any(x => x.Contains(" ")))
                throw new ArgumentException("Values cannot contain a space");

            _queryProperties[property] = values.Select(FormatString).Where(x => x != null).ToArray();
            return this;
        }

        private RedditSearchCriteria WithoutQueryProperty(string property, string[] values, bool allowSpaces)
        {
            if (values?.Any() != true)
                throw new ArgumentException("Values must be populated");

            if (values.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("All values must be populated");

            if (!allowSpaces && values.Any(x => x.Contains(" ")))
                throw new ArgumentException("Values cannot contain a space");

            _queryNotProperties[property] = values.Select(FormatString).Where(x => x != null).ToArray();
            return this;
        }

        private static string FormatString(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;

            var formatted = source.Trim();

            return formatted.Contains(" ")
                ? $"\"{formatted}\""
                : formatted;
        }
    }
}