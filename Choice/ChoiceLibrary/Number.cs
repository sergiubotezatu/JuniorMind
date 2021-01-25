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
            var digits = new OneOrMore(new Range('1', '9'));
            var zero = new Character('0');
            var minus = new Optional(new Character('-'));
            var decimalDot = new Character('.');
            var signs = new Optional(new Any("+-"));
            var exponential = new Any("eE");
            var minusZero = new Sequence(minus, zero);
            var integer = new Sequence(minus, digits, new Many(new Choice(zero, digits)));
            var decimalPart = new Sequence(new Range('0', '9'), new Many(zero), new List(digits, new Many(zero)));
            var fractional = new Sequence(decimalDot, decimalPart);
            var exponent = new Sequence(exponential, signs, digits, new Many(new Choice(digits, zero)));
            this.pattern = new Sequence(new Choice(integer, minusZero), new Optional(fractional), new Optional(exponent));
        }

        public IMatch Match(string text)
        {
            return this.pattern.Match(text);
        }
    }
}
