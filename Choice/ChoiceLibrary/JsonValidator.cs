using System;
using System.Collections.Generic;
using System.Text;
using ChoiceLibrary;
using InterFace;

namespace ChoiceLibrary
{
    class JsonValidator
    {
        static void Main()
        {
            string text = System.IO.File.ReadAllText(@"C: \Users\Sergiu95\Desktop\Json.Validator\JsonExample.txt");
            var validator = new Value();
            var result = validator.Match(text);
            string isValidJson = result.Success().ToString();
            string incorrectFormat = result.RemainingText();
            Console.WriteLine("{0} Remaining Text is: {1}", isValidJson, incorrectFormat);
            Console.Read();
        }
    }
}
