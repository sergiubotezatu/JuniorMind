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

            return ControlCharsAreMissing(input) && AreRecognizibleEscapedChars(input) && IsCorrectHexNumber(input);
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

               LookForWrongFormat(Unallowed, ref withoutQuotes, ref wrongNextCounter);
               if (wrongNextCounter == Unallowed.Length)
                {
                    return false;
                }
            }

            return true;
        }

        private static void LookForWrongFormat(string toBeEscaped, ref string withoutQuotes, ref int counter)
        {
            foreach (char element in toBeEscaped)
            {
                int indexOfNext = withoutQuotes.IndexOf('\\') + 1;
                if (withoutQuotes[indexOfNext] == element)
                {
                    EliminateCheckedChars(ref withoutQuotes, indexOfNext);
                    break;
                }

                counter++;
            }
        }

        private static void EliminateCheckedChars(ref string toBeModified, int indexOfelement)
        {
            toBeModified = toBeModified.Substring(indexOfelement + 1);
        }

        private static bool IsCorrectHexNumber(string input)
        {
            const int HexNumberLength = 4;
            bool containsHexInitiator = input.Contains('u') && input[input.IndexOf('u') - 1] == '\\';
            if (!containsHexInitiator)
            {
                return true;
            }

            return CheckAllHexNumbers(input.Substring(input.IndexOf('u') + 1), HexNumberLength);
        }

        private static bool CheckAllHexNumbers(string input, int hexNumberLength)
            {
            if (input.Length < hexNumberLength + 1)
            {
                return false;
            }
            else if (!CheckIfValidHexNumber(input.Substring(0, hexNumberLength)))
            {
                return false;
            }

            if (!input.Contains('u') || input[input.IndexOf('u') - 1] != '\\')
            {
                return true;
            }

            return IsCorrectHexNumber(input.Substring(hexNumberLength));
        }

        private static bool CheckIfValidHexNumber(string hexToCheck)
        {
            foreach (char element in hexToCheck)
            {
                if (!IsvalidHexChar(element))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsvalidHexChar(char toBechecked)
        {
            return InRange(toBechecked, '0', '9')
            || InRange(toBechecked, 'a', 'f')
            || InRange(toBechecked, 'A', 'F');
        }

        private static bool InRange(char toBeChecked, int lowerLimit, int upperLimit)
        {
            return lowerLimit <= toBeChecked && toBeChecked <= upperLimit;
        }
    }
}
