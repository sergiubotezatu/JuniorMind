using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
            {
            if (input == null || input.Length < 1)
            {
                return false;
            }

            return IsNumber(input);
        }

        private static bool IsNumber(string input)
        {
          for (int i = 0; i < input.Length; i++)
            {
                if (!IsCorrectDigit(input[i], i))
                {
                    return false;
                }
            }

          return input[0] != '0';
        }

        private static bool IsCorrectDigit(char digit, int inputIndex)
        {
            const string Digits = "0123456789";
            return inputIndex == 0 ? (Digits + "+-").Contains(digit) : Digits.Contains(digit);
        }
    }
}
