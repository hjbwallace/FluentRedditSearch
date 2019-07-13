using System;

namespace FluentRedditSearch.Tests
{
    public class TestFailedException : Exception
    {
        public TestFailedException(string iteration, Exception innerException)
            : base(GenerateIterationMessage(iteration, innerException), innerException)
        {
        }

        public TestFailedException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        private static string GenerateIterationMessage(string iteration, Exception inner)
        {
            return $"Failed on interation \"{iteration.ToUpper()}\": ";
        }
    }
}