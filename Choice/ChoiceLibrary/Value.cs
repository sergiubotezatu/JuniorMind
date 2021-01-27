using System;
using System.Collections.Generic;
using System.Text;
using InterFace;
using ChoiceLibrary;

namespace ChoiceLibrary
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var value = new Choice(
                    new String(),
                    new Number(),
                    new Text("true"),
                    new Text("false"),
                    new Text("null"));
            var whiteSpace = new Any("\n\r\t ");
            var objectEnd = new Sequence(whiteSpace, new Character('}'));
            var objectStart = new Sequence(new Character('{'), whiteSpace);
            var stringKey = new Sequence(new Character(','), whiteSpace, new String(), whiteSpace, new Text(":"));
            var filledObject = new Sequence(
                            objectStart,
                            new String(),
                            whiteSpace,
                            new Text(":"),
                            new List(value, stringKey),
                            objectEnd);
            var Object = new Choice(
                    filledObject,
                    new Sequence(
                    new Character('{'),
                    objectEnd));
            value.Add(Object);
            var emptyOrFilledArray = new Choice(new Character(' '), new List(value, new Text(", ")));
            var array = new Sequence(new Character('['), emptyOrFilledArray, new Character(']'));
            var arrayOfArrays = new OneOrMore(new Sequence(array, new Optional(new Text(", "))));
            value.Add(arrayOfArrays);
            this.pattern = value;
        }

        public IMatch Match(string text)
            {
                return this.pattern.Match(text);
            }
    }
}
