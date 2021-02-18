using System;
using System.Collections.Generic;
using System.Text;
using ArrayLibrary;
using Xunit;

namespace IntArrayFacts
{
    public class SortedIntArrayFacts
    {
        [Fact]
        public void SortsCorrectlyWhileAdding()
        {
            var sorted = new SortedIntArray();
            sorted.Add(4);
            sorted.Add(3);
            sorted.Add(1);
            sorted.Add(2);            
            Assert.True(sorted[0] == 1);
            Assert.True(sorted[1] == 2);
            Assert.True(sorted[2] == 3);
            Assert.True(sorted[3] == 4);
        }
    }
}
