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
            bool isNegative = input.First() == '-';
            if (isNegative)
            {
                toWorkWith = toWorkWith.Skip(1);
            }

            if (!IsConvertable(toWorkWith))
            {
                return false;
            }

            result = isNegative ? -1 * Convert(toWorkWith) : Convert(toWorkWith);
            return true;
        }

        private int Convert(IEnumerable<char> input)
        {
            return input.Select(x => ToUnit(x)).Aggregate(0, (result, number) => result * 10 + number);
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

        private int ToUnit(char toConvert)
        {
            return toConvert - '0';
        }

        private bool IsInRange(char toCheck, (char, char) range)
        {
            return toCheck >= range.Item1 && toCheck <= range.Item2;
        }        
    }
}
