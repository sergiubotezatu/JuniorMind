using System;
using ChoiceLibrary;
using InterFace;

namespace JsonValidator
{
    class Program
    {
        static void Main()
        {
                string text = System.IO.File.ReadAllText(@"C:\Users\Sergiu95\Desktop\curs.IT\JUNIORMINDGIT\Choice\JsonValidator\bin\Debug\netcoreapp3.1\JsonExample5.txt");
                var validator = new Value();
                var result = validator.Match(text);
                string isValidJson = result.Success().ToString();
                string incorrectFormat = result.RemainingText();
                Console.WriteLine(isValidJson + '\n' + incorrectFormat);
                Console.Read();            
        }
    }
}
