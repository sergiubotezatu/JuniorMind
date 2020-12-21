using System;
using System.Collections.Generic;
using System.Text;
using InterFace;
using ChoiceLibrary;

namespace ChoiceLibrary
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            var digit = new Range('0', '9');
            var nonZeroDigit = new Range('1', '9');
            var zero = new Character('0');
            var decimalPoint = new Character('.');
            var minus = new Optional(new Character('-'));
            var exponential = new Any("eE");
            var signs = new Optional(new Any("+-"));
            var decimals = new Sequence(new Many(zero), new List(new OneOrMore(nonZeroDigit), new OneOrMore(zero)));
            var leadingZero = new Choice(new Sequence(zero, decimalPoint, digit, decimals), zero);
            var integer = new Sequence(nonZeroDigit, new Many(digit));
            var rational = new Sequence(integer, decimalPoint, digit, decimals);
            var number = new Choice(leadingZero, rational, integer);
            var exponent = new Sequence(exponential, signs, nonZeroDigit, new Many(digit));
            this.pattern = new Sequence(minus, number, new Optional(exponent));
        }

        public IMatch Match(string text)
        {
            return this.pattern.Match(text);
        }
    }
}
