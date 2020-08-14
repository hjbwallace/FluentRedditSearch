using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FluentRedditSearch.Utilities
{
    internal static class QueryStringHelper
    {
        public static string GetQueryProperties(IDictionary<string, string[]> properties)
        {
            return string.Join("+", properties.Select(GetQueryProperty));
        }

        public static string GetApiProperties(IDictionary<string, string> properties)
        {
            var valuesString = properties.Select(x => $"&{Encode(x.Key)}={Encode(x.Value)}");
            return string.Join("", valuesString);
        }

        private static string GetQueryProperty(KeyValuePair<string, string[]> property)
        {
            return Encode($"{property.Key}:{GetValuesString(property.Value)}");
        }

        private static string GetValuesString(string[] values)
        {
            var valuesString = string.Join(" OR ", values);
            return values.Length > 1 ? $"({valuesString})" : valuesString;
        }

        private static string Encode(string source) => WebUtility.UrlEncode(source);
    }
}