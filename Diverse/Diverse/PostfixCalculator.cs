using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    class PostfixCalculator
    {
        public int CalculatePostfix(string[] input)
        {
            ThrowContainsInvalidChars(input);
            int signsCount = CountSigns(input);
            ThrowInvalidFormat(input, signsCount);
            return input.Aggregate(new List<int>(),(result, next) 
                => int.TryParse(next, out int value) ? 
                AddOperand(ref result, value) : 
                AddOperation(ref result, next)).First();
        }

        private List<int> AddOperand(ref List<int> infixed, int x)
        {
            infixed.Add(x);
            return infixed;
        }

        private List<int> AddOperation(ref List<int> infixed, string sign)
        {
            int size = infixed.Count();
            if (size < 2)
            {
                throw new InvalidOperationException("Wrong format of postfix expression.");
            }

            int right = infixed.Last();
            int left = infixed.TakeLast(2).First();
            infixed.RemoveRange(size - 2, 2);
            infixed.Add(Calculate(left, right, sign));
            return infixed;
        }

        private int Calculate(int left, int right, string sign)
        {
            int result = 0;
            switch (sign)
            {
                case "+":
                    result = left + right;
                    break;

                case "-":
                    result = left - right;
                    break;

                case "*":
                    result = left * right;
                    break;

                case "/":
                    result = left / right;
                    break;
            }

            return result;
        }


        private void ThrowContainsInvalidChars(string[] input)
        {
            if (input.Any(x => !int.TryParse(x, out _) || !"+-*/".Contains(x)) || input.Length < 3)
            {
                throw new InvalidOperationException($"{ nameof(input) }contains invalid chars.");
            }
        }

        private int CountSigns(IEnumerable<string> expression)
        {
            return expression.Aggregate(0, (result, next) => "+-*/".Contains(next) ? result++ : result);
        }

        private void ThrowInvalidFormat(string[] expression, int signsCount)
        {
            if (expression.Length - (expression.Length - signsCount) != 1)
            {
                throw new InvalidOperationException("Invalid postfix format");
            }            
        }
    }
}
