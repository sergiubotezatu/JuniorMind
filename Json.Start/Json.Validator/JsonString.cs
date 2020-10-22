using System;

namespace Json
{
    public static class JsonString
    {
        const int ControlCharLimit = 32;

        public static bool IsJsonString(string input)
        {
            if (string.IsNullOrEmpty(input) || !IsWrrapedInDoubleQuotes(input))
            {
                return false;
            }

            return ControlCharsAreMissing(input);
        }

        private static bool IsWrrapedInDoubleQuotes(string input)
        {
            return input[0] == '"' && input[input.Length - 1] == '"' && input.Length - 1 > 0;
        }

        private static bool ControlCharsAreMissing(string input)
        {
            for (int i = 1; i < input.Length - 1; i++)
            {
                if (input[i] < ControlCharLimit)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
