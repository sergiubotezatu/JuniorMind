using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return double.TryParse(input, out double number);
        }
    }
}
