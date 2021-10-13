using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse
{
    public class LetterCount
    {
        public (int, int) CountVowelsAndCons(string word)
        {
            var letters = word.Where(x => char.IsLetter(x));
            int vowelsNumber = letters.Aggregate(0, (count, character) => IsVowel(character) ? count + 1 : count);
            return ((letters.Count() - vowelsNumber), vowelsNumber);
        }

        private bool IsVowel(char letter)
        {
            string vowels = "aeiou";
            return vowels.Contains(letter);
        }
    }
}
