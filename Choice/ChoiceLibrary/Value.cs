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
            var element = new Sequence(whiteSpace, value, whiteSpace);
            var member = new Sequence(whiteSpace, new String(), whiteSpace, new Character(':'), element);
            var members = new List(member, new Character(','));
            var Object = new Sequence(new Character('{'), whiteSpace, members, whiteSpace, new Character('}'));
            value.Add(Object);
            var elements = new List(element, new Character(','));
            var array = new Sequence(new Character('['), whiteSpace, elements, whiteSpace, new Character(']'));
            value.Add(array);
            this.pattern = element;
        }

        public IMatch Match(string text)
            {
                return this.pattern.Match(text);
            }
    }
}
