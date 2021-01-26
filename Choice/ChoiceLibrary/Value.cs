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
            var anotherObject = new OneOrMore(new Sequence(new Text(", "), new String(), new Text(" :"), value));
            var objectStart = new Sequence(new Character('{'), new Character(' '));
            var objectEnd = new Sequence(new Character(' '), new Character('}'));
            var filledObject = new Sequence(
                objectStart,
                new String(),
                new Text(" :"),
                new Optional(value),
                new Optional(anotherObject),
                objectEnd);
            var Object = new Choice(filledObject, new Sequence(objectStart, new Character('}')));
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
