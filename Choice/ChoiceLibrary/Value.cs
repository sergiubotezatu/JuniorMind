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
            var whiteSpace = new Many(new Any("\n\r\t "));
            var member = new Sequence(whiteSpace, new String(), whiteSpace, new Character(':'), value);
            var members = new List(member, new Character(','));
            var objectEnd = new Sequence(whiteSpace, new Character('}'));
            var emptyObject = new Sequence(new Character('{'), whiteSpace, new Character('}'));
            var filledObject = new Sequence(new Character('{'), members, objectEnd);
            var Object = new Choice(emptyObject, filledObject);
            value.Add(Object);
            var emptyArray = new Sequence(new Character('['), whiteSpace, new Character(']'));
            var comma = new Sequence(new Character(','), whiteSpace);
            var filledArray = new Sequence(new Character('['), new List(value, comma), new Character(']'));
            var array = new Choice(emptyArray, filledArray);
            value.Add(array);
            this.pattern = value;
        }

        public IMatch Match(string text)
            {
                return this.pattern.Match(text);
            }
    }
}
