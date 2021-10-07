using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse
{
    public class LetterCount
    {
        public string CountVowelsAndCons(string word)
        {
            IEnumerable<char> characters = word.ToCharArray().AsEnumerable();
            var letters = characters.Where(x => char.IsLetter(x));
            int vowelsNumber = letters.Aggregate(0, (x, y) => CountVowels(x, y));
            return $"Your word contains {letters.Count() - vowelsNumber} consonants and {vowelsNumber} vowels";
        }

        private int CountVowels(int count, char letter)
        {
            IEnumerable<char> vowels = new List<char>() { 'a', 'e', 'i', 'o', 'u' };
            if (vowels.Contains(letter))
            {
                count += 1;
            }

            return count;

        }
    }
}
