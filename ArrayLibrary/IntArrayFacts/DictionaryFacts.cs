using System;
using System.Collections.Generic;
using System.Text;
using ArrayLibrary;
using Xunit;

namespace IntArrayFacts
{
    public class DictionaryFacts
    {
        [Fact]
        public void AddsCorrectlyAndConsecutivelyNewelements()
        {
            var testing = new ArrayLibrary.Dictionary<int, int>(5);
            testing.Add(0, 1);
            testing.Add(1, 2);
            testing.Add(2, 3);
            Assert.True(testing.TryGetValue(0, out int x));
            Assert.True(testing.TryGetValue(1, out int y));
            Assert.True(testing.TryGetValue(2, out int z));
            Assert.True(x == 1);
            Assert.True(y == 2);
            Assert.True(z == 3);
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
        public void AddsCorrectlyExtraLastNode()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            Node<int> test = new Node<int>(4);
            testing.AddLast(test);
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
            testing.AddBefore(testing.Find(2), 3);
            Assert.True(testing.Find(3).NextNode.Value == 2);
            Assert.True(testing.Find(3).PrevNode.Value == 1);
        }

        [Fact]
        public void AddsCorrectlyExtraElementAFTERRequestedElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddAfter(testing.Find(1), 3);
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
            Assert.True(testing.Find(2) == null);
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
            Assert.True(testing.Find(3) == null);
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
            Assert.True(testing.Find(1) == null);
        }

        [Fact]
        public void ThrowsCorrectlyNodeNotInCurrentList()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            Node<int> test = new Node<int>(4);
            string expectedMess = "Node is not in the current linked list";
            var exception = Assert.Throws<InvalidOperationException>(() => testing.AddAfter(test, 0));
            Assert.True(expectedMess.Equals(exception.Message));
        }
    }
}
