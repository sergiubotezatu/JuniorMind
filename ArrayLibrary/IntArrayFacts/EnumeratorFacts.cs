using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArrayLibrary;

namespace IntArrayFacts
{
    public class EnumeratorFacts
    {
        [Fact]
        public void MoveNExtIsTrueIfElementsStillNotEnumerated()
        {
            var test = new ObjectArray();
            test.Add(1);
            test.Add(2);
            test.Add(3);
            var enumerator = test.Enumerate().GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            bool checkValue = enumerator.MoveNext();
            Assert.True(checkValue);
        }

        [Fact]
        public void EnumerateReturnsFirstElementAfterFirstMoveNext()
        {
            var test = new ObjectArray();
            test.Add(1);
            test.Add(2);
            var enumerator = test.Enumerate().GetEnumerator();
            enumerator.MoveNext();
            Assert.True(enumerator.Current.Equals(1));
        }

        [Fact]
        public void EnumerateMovesCurrentEnumeratedElemCorrectly()
        {
            var test = new ObjectArray();
            test.Add(1);
            test.Add(2);
            var enumerator = test.Enumerate().GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            Assert.True(enumerator.Current.Equals(2));
        }

        [Fact]
        public void EnumerateReturnsFalseIfRunsOutOfMovesNext()
        {
            var test = new ObjectArray();
            test.Add(1);
            var enumerator = test.Enumerate().GetEnumerator();
            enumerator.MoveNext();
            bool checkValue = enumerator.MoveNext();
            Assert.False(checkValue);
        }
    }
}