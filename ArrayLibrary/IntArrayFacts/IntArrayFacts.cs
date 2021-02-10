using System;
using ArrayLibrary;
using Xunit;

namespace IntArrayFacts
{
    public class IntArrayTests
    {
        [Fact]
        public void ArrayHasFourElements()
        {
            var test = new IntArray();
            test.Add(1);
            test.Add(2);
            test.Add(3);
            test.Add(4);
            Assert.True(test.Count() == 4);
        }

        [Fact]
        public void AddsElementOnFirstEmptyPositions()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            Assert.True(test.Element(0) == 3);
            Assert.True(test.Element(1) == -1);
        }

        [Fact]
        public void CountsOnlyFilledPositions()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            Assert.True(test.Count() == 2);
        }

        [Fact]
        public void DoublesArrayCapacityAddingElementInExtraPos()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Add(6);
            Assert.True(test.Element(0) == 3);
            Assert.True(test.Element(1) == -1);
            Assert.True(test.Element(4) == 6);
            Assert.True(test.GetLength() == 8);
        }        

        [Fact]
        public void ReplacesCorrectlyOldValues()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.SetElement(0, -5);
            Assert.True(test.Element(0) == -5);
        }

        [Fact]
        public void ReturnsFalseIfElementIsNotInArray()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(2);
            test.Add(5);
            Assert.False(test.Contains(1));
        }

        [Fact]
        public void ReturnsTrueIfElementIsFounInArray()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(2);
            test.Add(5);
             Assert.True(test.Contains(2));
        }

        [Fact]
        public void ReturnsIndexOfElementFirstAppearance()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(2);
            test.Add(2);
            test.Add(5);
            Assert.True(test.IndexOf(2) == 1);
        }

        [Fact]
        public void ReturnsMinusOneIfElementIsNotInArray()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(2);
            test.Add(-4);
            test.Add(5);
            Assert.True(test.IndexOf(0) == -1);
        }

        [Fact]
        public void ReturnsMinusOneIfElemtIsNotInArray()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(2);
            test.Add(-4);
            test.Add(5);
            Assert.True(test.IndexOf(0) == -1);
        }

        [Fact]
        public void InsertsCorrectlyNewElementInEmptyPos()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Add(6);
            test.Insert(6, 10);
            Assert.True(test.Element(6) == 10);
        }

        [Fact]
        public void InsertsCorrectlyNewElementInFilledPos()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Insert(2, 10);
            Assert.True(test.Element(2) == 10);
        }

        [Fact]
        public void InsertsNewElementWhileAddingNewPos()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Insert(2, 10);
            Assert.True(test.GetLength() == 8);
        }

        [Fact]
        public void RemovesCorrectlyAllElements()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Clear();
            Assert.True(test.IndexOf(3) == -1);
            Assert.True(test.IndexOf(-1) == -1);
            Assert.True(test.IndexOf(4) == -1);
            Assert.True(test.IndexOf(0) == -1);
        }

        [Fact]
        public void RemovesCorrectlyRequestedElement()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Remove(4);
            Assert.True(test.IndexOf(4) == -1);
        }

        [Fact]
        public void RemovesCorrectlyElementFromIndicatedIndex()
        {
            var test = new IntArray();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.RemoveAt(3);
            Assert.True(test.IndexOf(0) == -1);
        }
    }
}
