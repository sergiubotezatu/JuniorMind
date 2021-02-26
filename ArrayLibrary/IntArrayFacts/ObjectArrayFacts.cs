﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArrayLibrary;

namespace IntArrayFacts
{
    public class ObjectArrayFacts
    {
        [Fact]
        public void ArrayHasFourElements()
        {
            var test = new ObjectCollection();
            test.Add(1);
            test.Add(2);
            test.Add(3);
            test.Add(4);
            int result = test.Count;
            Assert.True(result == 4);
        }

        [Fact]
        public void AddsElementOnFirstEmptyPositions()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            Assert.True(test[0].Equals(3));
            Assert.True(test[1].Equals(-1));
        }

        [Fact]
        public void CountsOnlyFilledPositions()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            int result = test.Count;
            Assert.True(result == 2);
        }

        [Fact]
        public void DoublesArrayCapacityAddingElementInExtraPos()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Add(6);
            Assert.True(test[0].Equals(3));
            Assert.True(test[1].Equals(-1));
            Assert.True(test[4].Equals(6));
        }

        [Fact]
        public void ReplacesCorrectlyOldValues()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test[0] = -5;
            Assert.True(test[0].Equals(-5));
        }

        [Fact]
        public void ReturnsFalseIfElementIsNotInArray()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(2);
            test.Add(5);
            Assert.False(test.Contains(1));
        }

        [Fact]
        public void ReturnsTrueIfElementIsFounInArray()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(2);
            test.Add(5);
            Assert.True(test.Contains(2));
        }

        [Fact]
        public void ReturnsIndexOfElementFirstAppearance()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(2);
            test.Add(2);
            test.Add(5);
            Assert.True(test.IndexOf(2) == 1);
        }

        [Fact]
        public void ReturnsMinusOneIfElementIsNotInArray()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(2);
            test.Add(-4);
            test.Add(5);
            Assert.True(test.IndexOf(0) == -1);
        }

        [Fact]
        public void ReturnsMinusOneIfElemtIsNotInArray()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(2);
            test.Add(-4);
            test.Add(5);
            Assert.True(test.IndexOf(0) == -1);
        }

        [Fact]
        public void InsertsCorrectlyNewElementInEmptyPos()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Add(6);
            test.Insert(5, 10);
            Assert.True(test[5].Equals(10));
        }

        [Fact]
        public void InsertsCorrectlyNewElementInFilledPos()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Insert(2, 10);
            Assert.True(test[2].Equals(10));
        }

        [Fact]
        public void InsertsNewElementWhileAddingNewPos()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(1);
            test.Insert(2, 10);
            Assert.True(test.IndexOf(1) == 4);
        }

        [Fact]
        public void RemovesCorrectlyAllElements()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Clear();
            test.Add(4);
            test.Add(0);
            Assert.True(test.IndexOf(4) == 0);
            Assert.True(test.IndexOf(0) == 1);
        }

        [Fact]
        public void RemovesCorrectlyRequestedElement()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.Remove(4);
            Assert.True(test.IndexOf(3) == 0);
            Assert.True(test.IndexOf(-1) == 1);
            Assert.True(test.IndexOf(0) == 2);
        }

        [Fact]
        public void RemovesCorrectlyElementFromIndicatedIndex()
        {
            var test = new ObjectCollection();
            test.Add(3);
            test.Add(-1);
            test.Add(4);
            test.Add(0);
            test.RemoveAt(0);
            Assert.True(test.IndexOf(3) == -1);
        }
    }
}

