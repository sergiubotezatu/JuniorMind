using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class String : IPattern
    {
        private readonly IPattern pattern;

        public String()
        {
            var greaterThanEscape = new Range((char)35, '\uFFFF');
            var exclamationMark = new Character('!');
            var acceptedChars = new Choice(exclamationMark, greaterThanEscape);
            var quotedEmpty = new Text("\"\"");
            var escapedChars = new Any("/\"\\bfnrt");
            var quote = new Character('\"');
            var escape = new Text("\\");
            var digit = new Range('0', '9');
            var lowerCase = new Range('a', 'f');
            var upperCase = new Range('A', 'F');
            var hex = new Choice(digit, lowerCase, upperCase);
            var correctEscape = new Sequence(escape, escapedChars);
            var hexNumber = new Sequence(escape, new Character('u'), hex, hex, hex, hex);
            var actualString = new Many(new Choice(correctEscape, hexNumber, acceptedChars));
            var firstSequence = new Sequence(quote, actualString);
            this.pattern = new Choice(new Sequence(firstSequence, quote), firstSequence);
        }

        public IMatch Match(string text)
        {
            return this.pattern.Match(text);
        }
    }
}
