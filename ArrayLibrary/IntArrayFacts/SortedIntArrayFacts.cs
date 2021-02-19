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

        [Fact]
        public void SortsCorrectlyWhileAddingSameElement()
        {
            var sorted = new SortedIntArray();
            sorted.Add(4);
            sorted.Add(3);
            sorted.Add(2);
            sorted.Add(2);
            Assert.True(sorted[0] == 2);
            Assert.True(sorted[1] == 2);
            Assert.True(sorted[2] == 3);
            Assert.True(sorted[3] == 4);
        }

        [Fact]
        public void RejectsInsertIfElementIsNotInCorrectPos()
        {
            var sorted = new SortedIntArray();
            sorted.Add(4);
            sorted.Add(3);
            sorted.Add(1);
            sorted.Add(2);
            sorted.Insert(1, 5);
            Assert.True(sorted.IndexOf(5) == -1);
        }

        [Fact]
        public void WorksOnlyIfElementIsInCorrectPos()
        {
            var sorted = new SortedIntArray();
            sorted.Add(4);
            sorted.Add(3);
            sorted.Add(0);
            sorted.Add(2);
            sorted.Insert(1, 1);
            Assert.True(sorted.IndexOf(1) == 1);
        }

        [Fact]
        public void WorksIfAlreadyExistingElemIsInserted()
        {
            var sorted = new SortedIntArray();
            sorted.Add(4);
            sorted.Add(3);
            sorted.Add(1);
            sorted.Add(2);
            sorted.Insert(1, 1);
            Assert.True(sorted[0] == 1);
            Assert.True(sorted[1] == 1);
        }
    }
}
