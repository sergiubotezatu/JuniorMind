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
            var digits = new OneOrMore(new Range('0', '9'));
            var zero = new Character('0'); 
            var minus = new Optional(new Character('-'));
            var decimalDot = new Character('.');
            var signs = new Optional(new Any("+-"));
            var exponential = new Any("eE");
            var integer = new Sequence(minus, new Choice(zero, digits));
            var fractional = new Sequence(decimalDot, digits);
            var exponent = new Sequence(exponential, signs, new Range('1','9'), new Optional(digits));
            this.pattern = new Sequence(integer, new Optional(fractional), new Optional(exponent));
        }

        public IMatch Match(string text)
        {
            return this.pattern.Match(text);
        }
    }
}
