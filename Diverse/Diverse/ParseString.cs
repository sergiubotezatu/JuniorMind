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
            IEnumerable<char> toWorkWith = input;
            Func<IEnumerable<char>, int, int> ToInteger = Convert;
            if (input.First() == '-')
            {
                toWorkWith = toWorkWith.Skip(1);
                ToInteger = ConvertNegative;
            }

            if (!IsConvertable(toWorkWith))
            {
                return false;
            }

            result = ToInteger(toWorkWith, toWorkWith.Count() - 1);
            return true;
        }

        private int Convert(IEnumerable<char> input, int digits)
        {
            return input.Select(x => ToTens(x, digits--)).Aggregate(0, (result, number) => result + number);
        }

        private int ConvertNegative(IEnumerable<char> toConvert, int digits)
        {
            return 0 - Convert(toConvert, digits);
        }

        private bool IsConvertable(IEnumerable<char> toCheck)
        {
            char lead = toCheck.First();
            if (toCheck.Count() == 1)
            {
                return IsInRange(lead, ('0', '9'));
            }

            return IsInRange(lead, ('1', '9')) && toCheck.Skip(1).All(x => IsInRange(x, ('0','9')));
        }       

        private int ToTens(char toMultiply, int exponent)
        {
            if (toMultiply == '0')
            {
                return 0;
            }

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

        private bool IsInRange(char toCheck, (char, char) range)
        {
            return toCheck >= range.Item1 && toCheck <= range.Item2;
        }        
    }
}
