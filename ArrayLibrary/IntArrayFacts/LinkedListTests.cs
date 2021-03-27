using Xunit;
using ArrayLibrary;
using System;

namespace IntArrayFacts
{
    public class LinkedListTests
    {
        [Fact]
        public void AddsCorrectlyAndConsecutivelyNewelements()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            Assert.True(testing.Count == 2);
            Assert.True(testing.Find(1).NextNode.Value == 2);
            Assert.True(testing.Find(2).PrevNode.Value == 1);
        }

        [Fact]
        public void AddsCorrectlyExtraLastElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddLast(4);
            Assert.True(testing.Find(4).PrevNode.Value == 2);            
        }

        [Fact]
        public void AddsCorrectlyExtraFirstElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddFirst(4);
            Assert.True(testing.Find(4).NextNode.Value == 1);
        }

        [Fact]
        public void AddsCorrectlyExtraElementBEFORERequestedElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddBefore(3, 2);
            Assert.True(testing.Find(3).NextNode.Value == 2);
            Assert.True(testing.Find(3).PrevNode.Value == 1);
        }

        [Fact]
        public void AddsCorrectlyExtraElementAFTERRequestedElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddAfter(3, 1);
            Assert.True(testing.Find(3).NextNode.Value == 2);
            Assert.True(testing.Find(3).PrevNode.Value == 1);
        }

        [Fact]
        public void ClearRemovesCorrectlyAllElements()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.Clear();
            Assert.True(testing.Count == 0);
            Assert.DoesNotContain(2, testing);
        }

        [Fact]
        public void ContainsReturnsTrueIfTheItemIsFoundInTheList()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            Assert.Contains(2, testing);
        }        

        [Fact]
        public void RemovesCorrectlyRequestedItem()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.Remove(2);
            Assert.True(testing.Count == 2);
            var exception = Assert.Throws<InvalidOperationException>(() => testing.Find(2));
            Assert.True(exception.Message == "The node you are searching for does not exist in this list.");
        }

        [Fact]
        public void RemovesCorrectlyLastElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.RemoveLast();
            Assert.True(testing.Count == 2);
            var exception = Assert.Throws<InvalidOperationException>(() => testing.Find(3));
            Assert.True(exception.Message == "The node you are searching for does not exist in this list.");
        }

        [Fact]
        public void RemovesCorrectlyFirstElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.RemoveFirst();
            Assert.True(testing.Count == 2);
            var exception = Assert.Throws<InvalidOperationException>(() => testing.Find(1));
            Assert.True(exception.Message == "The node you are searching for does not exist in this list.");
        }        
    }
}
