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
            const int AlreadyChecked = 2;
            var withoutQuotes = input[1..^1];
            while (withoutQuotes.Contains('\\') && withoutQuotes.Length >= 1)
            {
                if (withoutQuotes.IndexOf('\\') == withoutQuotes.Length - 1 || !Unallowed.Contains(withoutQuotes[withoutQuotes.IndexOf('\\') + 1]))
                {
                    return false;
                }

                withoutQuotes = withoutQuotes.Substring(withoutQuotes.IndexOf('\\') + AlreadyChecked);
            }

            return true;
        }

        private static bool IsCorrectHexNumber(string input)
        {
            int[] hexIndexes = GetHexIndexes(input);
            if (hexIndexes.Length == 0)
            {
                return true;
            }

            return CheckAllHexNumbers(input, hexIndexes);
        }

        private static int[] GetHexIndexes(string input)
        {
            int[] result = new int[0];
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == 'u' && input[i - 1] == '\\')
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = i + 1;
                }
            }

            return result;
        }

        private static bool CheckAllHexNumbers(string input, int[] startingPoints)
        {
            const int HexNumberLength = 4;
            foreach (int index in startingPoints)
            {
                if (input.Length - index <= HexNumberLength)
                {
                    return false;
                }

                if (!CheckIfValidHexNumber(input.Substring(index, HexNumberLength)))
                {
                    return false;
                }
            }

            return true;
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
