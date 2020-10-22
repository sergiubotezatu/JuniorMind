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

            return ControlCharsAreMissing(input) && AreRecognizibleEscapedChars(input) && IsCorrectOrUnfinishedHexNumber(input);
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

        private static bool IsCorrectOrUnfinishedHexNumber(string input)
        {
            const int HexNumberLength = 5;
            bool containsHexInitiator = input.IndexOf('u') != -1 && input[input.IndexOf('u') - 1] == '\\';
            if (!containsHexInitiator)
            {
                return true;
            }

            if (input.Length <= HexNumberLength)
            {
                return false;
            }

            int lastPossibleHex = input.Length - HexNumberLength;
            for (int currentChar = 1; currentChar <= lastPossibleHex; currentChar++)
            {
                if (input[currentChar] == 'u' && input[currentChar - 1] == '\\')
                {
                    if (input.Substring(input.IndexOf('u') + 1).Length < HexNumberLength - 1)
                    {
                        return false;
                    }
                    else if (!CheckIfValidHexNumber(input.Substring(input.IndexOf('u') + 1, HexNumberLength - 1)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool CheckIfValidHexNumber(string hexToCheck)
        {
            const int CapitalLowerLimit = 65;
            const int CapitalUpperLimit = 70;
            const int LowerCaseLowwerLimit = 97;
            const int LowerCaseUpperLimit = 102;
            foreach (char element in hexToCheck)
            {
                bool isLowerCaseHex = element > LowerCaseLowwerLimit && element < LowerCaseUpperLimit;
                bool isCapitalHex = element > CapitalLowerLimit && element < CapitalUpperLimit;
                bool isValidLetter = isLowerCaseHex || isCapitalHex;
                bool isNumber = int.TryParse(element.ToString(), out int digit);
                if (!isNumber && !isValidLetter)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
