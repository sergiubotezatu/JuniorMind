using System;
using System.Collections.Generic;
using System.Text;
using ChoiceLibrary;
using InterFace;

namespace ChoiceLibrary
{
    public class DateTime : IPattern
    {
        private readonly IPattern pattern;

        public DateTime()
        {
            var whiteSpace = new Many(new Any("\n\r\t "));
            var commentIntervals = new Choice(
                new Range((char)33, (char)39),
                new Range((char)42, (char)91),
                new Range((char)93, (char)126));
            var comment = new Sequence(new Character('('),
                whiteSpace,
                new OneOrMore(commentIntervals),
                whiteSpace,
                new Character(')'));
            var commentWhiteSpace = new Optional(new Sequence(whiteSpace, comment, whiteSpace));
            var colon = new Text(":");
            var signGreenwich = new Any("+-");
            var comma = new Text(",");
            var digit = new Range('0', '9');
            var twoDigits = new Sequence(digit, digit);
            var weekDays = new Choice(
                new Text("Mon"),
                new Text("Tue"),
                new Text("Wed"),
                new Text("Thu"),
                new Text("Fri"),
                new Text("Sat"),
                new Text("Sun"));
            var day = new Sequence(whiteSpace, weekDays, comma);
            var months = new Choice(
                new Text("Jan"),
                new Text("Feb"),
                new Text("Mar"),
                new Text("Apr"),
                new Text("May"),
                new Text("Jun"),
                new Text("Jul"),
                new Text("Aug"),
                new Text("Sep"),
                new Text("Oct"),
                new Text("Nov"),
                new Text("Dec"));
            var year = new Sequence(twoDigits, twoDigits);
            var date = new Sequence(whiteSpace, twoDigits, whiteSpace, months, whiteSpace, year, whiteSpace);
            var timeOfDay = new Sequence(twoDigits, colon, twoDigits, new Optional(new Sequence(colon, twoDigits)));
            var zone = new Sequence(whiteSpace, signGreenwich, twoDigits, twoDigits);
            var time = new Sequence(timeOfDay, zone);
            this.pattern = new Sequence(day, date, time, commentWhiteSpace)


        }

        public IMatch Match(string text)
        {
            return this.pattern.Match(text);
        }
    }
}
