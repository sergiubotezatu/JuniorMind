using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class ParseString
    {
        public bool TryConvertToInt(string input, out int result)
        {
            result = 0;
            Func<IEnumerable<char>, int, int> ToInteger = Convert;
            if (!IsCorrectLead(input, out char lead))
            {
                return false;
            }

            IEnumerable<char> toWorkWith = GetDigitsOnly(input, lead, ref ToInteger);
            if(toWorkWith.Count() != input.Length - 1)
            {
                return false;
            }

            int tens = toWorkWith.Count() - 1;
            result = ToInteger(toWorkWith, tens);
            return true;
        }

        private IEnumerable<char> GetDigitsOnly(string toConvert, char first, ref Func<IEnumerable<char>, int, int> method)
        {
            IEnumerable<char> result;
            if (first == '-')
            {
                result = toConvert.Skip(1).Where(x => IsNumeric(x));
                method = ConvertNegative;
            }
            else
            {
                result = toConvert.Where(x => IsNumeric(x));
            }

            return result;
        }

        private int ConvertNegative(IEnumerable<char> toConvert, int digits)
        {
            return 0 - Convert(toConvert, digits);
        }

        private int Convert(IEnumerable<char> toConvert, int digits)
        {
            return toConvert.Aggregate(0, (number, character) => number + ToTens(character, digits--));
        }

        private int ToTens(char toMultiply, int exponent)
        {
            return ToUnit(toMultiply) * TenToThe(exponent);
        }

        private int ToUnit(char toConvert)
        {
            return toConvert - '0';
        }

        private int TenToThe(int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= 10;
            }

            return result;
        }

        private bool IsCorrectLead(string toCheck, out char lead)
        {
            lead = toCheck.First();
            if (lead == '-' && toCheck.Length > 1)
            {
                return IsInRange(toCheck.Skip(1).First(), ('1', '9'));
            }

            if (toCheck.Length == 1)
            {
                return IsInRange(lead, ('0', '9'));
            }

            return lead >= '1' && lead <= '9';
        }

        private bool IsNumeric (char toCheck)
        {
            return IsInRange(toCheck, ('0', '9'));
        }

        private bool IsInRange(char toCheck, (char, char) range)
        {
            return toCheck >= range.Item1 && toCheck <= range.Item2;
        }
    }
}
