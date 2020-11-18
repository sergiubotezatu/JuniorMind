using System;
using System.Collections.Generic;
using System.Text;

namespace ChoiceClass
{
    public interface IPattern
    {
        bool Match(string text);
    }

    public class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public bool Match(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            return text[0] == pattern;
        }
    }

    public class Choice : IPattern
    {
        IPattern[] patterns;
        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public bool Match(string text)
        {
            foreach (var pattern in patterns)
            {
                if (pattern.Match(text))
                {
                    return true;
                }
            }

            return false;
        }
    }

        public class Range : IPattern
    {
        private readonly char start;
        private readonly char end;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public bool Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return this.IsInRange(text[0]);
        }

        private bool IsInRange(char firstChar)
        {
            return firstChar >= this.start && firstChar <= this.end;
        }
    }   

    public class Program
    {
        static void Main()
        {
        }
    }
}
