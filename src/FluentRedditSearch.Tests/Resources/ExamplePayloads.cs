using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentRedditSearch.Tests.Resources
{
    internal static class ExamplePayloads
    {
        private static IDictionary<string, PayloadContainer> _payloadDictionary;

        public static IDictionary<string, PayloadContainer> Get()
        {
            if (_payloadDictionary == null)
                _payloadDictionary = GeneratePayloadDictionary();

            return _payloadDictionary;
        }

        public static PayloadContainer Get(string key)
        {
            var payloads = Get();
            return payloads[key];
        }

        private static IDictionary<string, PayloadContainer> GeneratePayloadDictionary()
        {
            var searchDictionary = GetPayloadFiles("Search");
            var resultsDictionary = GetPayloadFiles("Results");

            searchDictionary.Keys.Should().BeEquivalentTo(resultsDictionary.Keys);

            var keys = searchDictionary.Keys.Concat(resultsDictionary.Keys).Distinct();

            return keys.ToDictionary(key => key, value => new PayloadContainer
            {
                Search = searchDictionary[value],
                Results = resultsDictionary[value]
            });
        }

        private static IDictionary<string, string> GetPayloadFiles(string type)
        {
            var fileFormat = $"{type}.json";

            return Directory
                .GetFiles(@"Resources\Payloads\", $"*{fileFormat}")
                .ToDictionary(
                    key => new FileInfo(key).Name.Replace(fileFormat, ""),
                    value => File.ReadAllText(value));
        }
    }
}