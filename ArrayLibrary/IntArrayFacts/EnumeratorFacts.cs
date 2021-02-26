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
        public void Test1()
        {
            var test = new ObjectCollection();
            test.Add(1);
            test.Add(2);
            var enumerator = test.GetEnumerator();
            Assert.True(enumerator.Current.Equals(1));
        }

        [Fact]
        public void Test3()
        {
            var test = new ObjectCollection();
            test.Add(1);
            test.Add(2);
            var enumerator = test.GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.Reset();
            Assert.True(enumerator.Current.Equals(1));
        }

        [Fact]
        public void Test4()
        {
            var test = new ObjectCollection();
            var enumerator = test.GetEnumerator();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            Assert.False(enumerator.MoveNext());
        }        
    }
}
