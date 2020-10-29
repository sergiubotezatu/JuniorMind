using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            bool isNumber = double.TryParse(input, out double number);
            if (input == null || input.Length <= 1)
            {
                return isNumber;
            }

            return !input.Contains('.') ? input[0] != '0' && isNumber : input[input.Length - 1] != '.' && isNumber;
        }
    }
}
