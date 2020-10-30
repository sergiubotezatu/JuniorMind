using System;

namespace Json
{
    public static class JsonNumber
    {
        const string Digits = "0123456789";

        public static bool IsJsonNumber(string input)
            {
            if (input == null || input.Length < 1)
            {
                return false;
            }

            return IsCorrectFraction(input) && IsNumber(input);
        }

        private static bool IsNumber(string input)
        {
          if (!IsValidFirstDigit(input))
            {
                return false;
            }

          for (int i = 1; i < input.Length; i++)
            {
                if (!IsCorrectDigit(input[i], i))
                {
                    return false;
                }
            }

          return true;
        }

        private static bool IsValidFirstDigit(string input)
        {
            if (input.Contains('.'))
            {
                return (Digits + "+-").Contains(input[0]);
            }

            string digitsWithoutZero = Digits.Substring(1) + "+-";
            return input.Length > 1 ? digitsWithoutZero.Contains(input[0]) : Digits.Contains(input[0]);
        }

        private static bool IsCorrectFraction(string input)
        {
            return input.IndexOf('.') == input.LastIndexOf('.') && input.IndexOf('.') != 0
                && input.IndexOf('.') != input.Length - 1;
        }

        private static bool IsCorrectDigit(char digit, int inputIndex)
            {
            return (Digits + '.').Contains(digit);
            }
    }
}
