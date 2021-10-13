using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse
{
    public class LetterCount
    {
        public (int, int) CountVowelsAndCons(string word)
        {
            IEnumerable<char> characters = word.ToCharArray().AsEnumerable();
            var letters = characters.Where(x => char.IsLetter(x));
            int vowelsNumber = letters.Aggregate(0, (count, character) => IsVowel(character) ? count + 1 : count);
            (int consonants, int vowels) result = ((letters.Count() - vowelsNumber), vowelsNumber);
            return result;
        }

        private bool IsVowel(char letter)
        {
            string vowels = "aeiou";
            return vowels.Contains(letter);
        }
    }
}
