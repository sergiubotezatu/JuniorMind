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

            return ControlCharsAreMissing(input) && AreRecognizibleEscapedChars(input);
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

        private static bool AreRecognizibleEscapedChars(string input)
        {
            const int NextSubstring = 2;
            if (input.Length <= NextSubstring)
            {
                return input[0] != '\\';
            }

            int correctNextChar = 0;
            char[] unallowed = { '\\', '"', '/', 'b', 'f', 'n', 'r', 't', 'u' };
            if (input[0] == '\\')
            {
                foreach (char element in unallowed)
                {
                    if (input[1] == element)
                    {
                        correctNextChar++;
                    }

                    if (correctNextChar > 0)
                    {
                        break;
                    }
                }

                if (correctNextChar == 0)
                {
                    return false;
                }
            }

            if (correctNextChar > 0)
            {
                return AreRecognizibleEscapedChars(input.Substring(NextSubstring));
            }

            return AreRecognizibleEscapedChars(input.Substring(1));
        }
    }
}
