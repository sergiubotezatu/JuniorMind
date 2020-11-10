using System;

namespace Json
{
    public static class JsonNumber
    {
        const string Digits = "0123456789";
        const char Fraction = '.';
        const char Exponent = 'e';

        public static bool IsJsonNumber(string input)
            {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (!input.ToLower().Contains(Exponent))
            {
                return CorrectNonDigitDistribution(input, Fraction) && IsNumber(input);
            }

            return CorrectNonDigitDistribution(input, Fraction) && IsCorrectExponent(input.ToLower());
        }

        private static bool IsCorrectExponent(string input)
        {
              return CorrectNonDigitDistribution(input, Exponent)
                && ExponentFlankedByCorrectNumbers(input, input.IndexOf(Exponent));
        }

        private static bool ExponentFlankedByCorrectNumbers(string input, int positionOfExpo)
        {
            string leftSide = input.Substring(0, positionOfExpo);
            string rightSide = input.Substring(positionOfExpo + 1);
            if (leftSide[leftSide.Length - 1] == Fraction || rightSide.Contains(Fraction))
            {
                return false;
            }

            return IsNumber(leftSide) && IsNumber(rightSide);
        }

        private static bool IsNumber(string input)
        {
          if (!IsValidFirstDigit(input))
            {
                return false;
            }

          for (int i = 1; i < input.Length; i++)
            {
                if (!IsCorrectDigitOrSign(input[i]))
                {
                    return false;
                }
            }

          return true;
        }

        private static bool IsValidFirstDigit(string input)
        {
            if (input.Contains(Fraction))
            {
                return (Digits + "+-").Contains(input[0]);
            }

            string digitsWithoutZero = Digits.Substring(1) + "+-";
            return input.Length > 1 ? digitsWithoutZero.Contains(input[0]) : Digits.Contains(input[0]);
        }

        private static bool CorrectNonDigitDistribution(string input, char nonDigit)
        {
           return input.IndexOf(nonDigit) == input.LastIndexOf(nonDigit) && input.IndexOf(nonDigit) != 0
                && input.IndexOf(nonDigit) != input.Length - 1;
        }

        private static bool IsCorrectDigitOrSign(char digit)
            {
            return (Digits + Fraction + Exponent).Contains(digit);
            }
    }
}
