using System;
using System.Collections.Generic;
using Diverse;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            string test = Console.ReadLine();
            PalindromeValidator result = new PalindromeValidator();
            IEnumerable<string> res = result.GetPalindrome(test);
            Console.WriteLine(res);
        }
    }
}
