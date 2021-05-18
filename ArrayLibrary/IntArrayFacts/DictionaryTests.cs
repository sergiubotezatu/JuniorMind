using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArrayLibrary;

namespace IntArrayFacts
{
    public class DictionaryTests
    {
        [Fact]
        public void AddsItemOnCorrectPosIfKeyIsLessThanCapacity()
        {
            var dictionary = new ArrayLibrary.Dictionary<int, int>(5);
            dictionary.Add(0, 1);
            ListCollection<int> values = (ListCollection<int>)dictionary.Values;
            Assert.True(values[0] == 1);
        }

        [Fact]
        public void AddsItemOnCorrectPosIfKeyIsGreaterThanCapacity()
        {
            var dictionary = new ArrayLibrary.Dictionary<int, int>(5);
            dictionary.Add(10, 1);
            ListCollection<int> values = (ListCollection<int>)dictionary.Values;
            Assert.True(values[0] == 1);
        }

        [Fact]
        public void AddsItemOnCorrectPosMultiplItems()
        {
            var dictionary = new ArrayLibrary.Dictionary<int, int>(5);
            dictionary.Add(0, 1);
            dictionary.Add(1, 2);
            dictionary.Add(2, 3);
            dictionary.Add(3, 4);
            dictionary.Add(4, 5);
            ListCollection<int> values = (ListCollection<int>)dictionary.Values;
            Assert.True(values[0] == 1);
            Assert.True(values[1] == 2);
            Assert.True(values[2] == 3);
            Assert.True(values[3] == 4);
            Assert.True(values[4] == 5);
        }

        [Fact]
        public void IdentifiesCollisionAndPlacesItemCorrectly()
        {
            var dictionary = new ArrayLibrary.Dictionary<int, int>(3);
            dictionary.Add(0, 1);
            dictionary.Add(1, 2);
            dictionary.Add(2, 3);
            dictionary.Add(6, 4);
            ListCollection<int> keys = (ListCollection<int>)dictionary.Keys;
            Assert.True(keys[0] == 0);
            Assert.True(keys[1] == 6);
        }
    }
}
