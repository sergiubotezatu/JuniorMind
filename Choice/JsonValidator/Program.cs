using System;
using ChoiceLibrary;
using InterFace;

namespace JsonValidator
{
    class Program
    {
        static void Main()
        {
                Console.WriteLine("Enter Json text path:");
                string text = System.IO.File.ReadAllText(Console.ReadLine());
                var validator = new Value();
                var result = validator.Match(text);
                string output = "Your text respects Json format entirely";
                if (result.RemainingText().Length < text.Length && result.RemainingText().Length > 0)
                {
                    output = "Part of your text respects Json format. This is not valid Json: \n" + result.RemainingText();
                }

                if (!result.Success())
                {
                    output = "Your text does not respect Json format \n" + result.RemainingText();
                }

                Console.WriteLine(output);
                Console.Read();            
        }
    }
}
