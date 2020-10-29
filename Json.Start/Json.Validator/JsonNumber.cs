using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return IsNumber(input);
        }

        private static bool IsNumber(string input)
        {
            const string Digits = "0123456789";
            foreach (char digit in input)
            {
                if (!Digits.Contains(digit))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
