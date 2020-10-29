using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            if (input == null || input.Length <= 1)
            {
                return double.TryParse(input, out double number);
            }

            return !input.Contains('.') ? input[0] != '0' && TryParseWithExponenet(input)
                : input[input.Length - 1] != '.' && TryParseWithExponenet(input);
        }

        private static bool TryParseWithExponenet(string input)
        {
            if (input.Contains("e", StringComparison.CurrentCultureIgnoreCase))
            {
                int positionOfExponent = input.IndexOf('e'.ToString(), StringComparison.CurrentCultureIgnoreCase);
                int sameExponentPosition = input.LastIndexOf('e'.ToString(), StringComparison.CurrentCultureIgnoreCase);
                bool onlyOneExponent = positionOfExponent == sameExponentPosition;
                return onlyOneExponent && AreValidLeftAndRight(input, positionOfExponent);
            }

            return double.TryParse(input, out double number);
        }

        private static bool AreValidLeftAndRight(string input, int positionOfExponent)
        {
            const string PermittedNonDigits = "+-";
            string exponenetLeft = input.Substring(0, positionOfExponent);
            char sign = input[positionOfExponent + 1];
            positionOfExponent += PermittedNonDigits.Contains(sign) ? 1 : 0;
            string exponenetRight = input.Substring(positionOfExponent + 1);
            return double.TryParse(exponenetLeft, out double number) && int.TryParse(exponenetRight, out int exponenet);
        }
    }
}
