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
            return input.Length - 1 > 0 && input[0] == '"' && input[input.Length - 1] == '"';
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
            const string Unallowed = "\\\"/bfnrtu";
            int wrongNextCounter = 0;
            string withoutQuotes = input.Substring(1).Remove(input.Substring(1).Length - 1);
            while (withoutQuotes.Contains('\\') && withoutQuotes.Length >= 1)
            {
               if (withoutQuotes.IndexOf('\\') == withoutQuotes.Length - 1)
                {
                    return false;
                }

               foreach (char element in Unallowed)
                {
                    int indexOfNext = withoutQuotes.IndexOf('\\') + 1;
                    if (withoutQuotes[indexOfNext] == element)
                    {
                        EliminateCheckedChars(ref withoutQuotes, indexOfNext);
                        break;
                    }

                    wrongNextCounter++;
                }

               if (wrongNextCounter == Unallowed.Length)
                {
                    return false;
                }
            }

            return true;
        }

        private static void EliminateCheckedChars(ref string toBeModified, int indexOfelement)
        {
            toBeModified = toBeModified.Substring(indexOfelement + 1);
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
