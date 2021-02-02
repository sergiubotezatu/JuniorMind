using System;
using System.Collections.Generic;
using ChoiceLibrary;

namespace JsonValidator
{
    class Program
    {
        private static void PrintResult(string argument)
        {
            string text = System.IO.File.ReadAllText(@argument);
            var validator = new Value();
            var result = validator.Match(text);
            string output = "Your text respects Json format entirely";
            if (result.RemainingText().Length < text.Length && result.RemainingText().Length > 0)
            {
                output = "Part of your text respects Json format. This is not valid Json: \n" + result.RemainingText();
            }

            if (!result.Success())
            {
                output = "Your text does not respect Json format";
            }

            Console.WriteLine(output);
        }

        static void Main(string[] args)
        {
            foreach (string path in args)
            {
                PrintResult(path);
            }

            Console.Read();
        }
    }
}
