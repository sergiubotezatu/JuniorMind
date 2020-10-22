using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return !string.IsNullOrEmpty(input) && IsWrrapedInDoubleQuotes(input);
        }

        private static bool IsWrrapedInDoubleQuotes(string input)
        {
            return input[0] == '"' && input[input.Length - 1] == '"' && input.Length - 1 > 0;
        }
    }
}
