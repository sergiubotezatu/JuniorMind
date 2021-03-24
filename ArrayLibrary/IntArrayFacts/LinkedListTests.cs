using Xunit;

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
            Assert.True(testing.GetItemPosition(1) == 0);
            Assert.True(testing.GetItemPosition(2) == 1);
        }

        [Fact]
        public void AddsCorrectlyExtraLastElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddLast(4);
            Assert.True(testing.GetItemPosition(4) == 2);            
        }

        [Fact]
        public void AddsCorrectlyExtraFirstElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddFirst(4);
            Assert.True(testing.GetItemPosition(4) == 0);
        }

        [Fact]
        public void AddsCorrectlyExtraElementBEFORERequestedElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddBefore(3, 2);
            Assert.True(testing.GetItemPosition(3) == 1);
        }

        [Fact]
        public void AddsCorrectlyExtraElementAFTERRequestedElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.AddAfter(3, 1);
            Assert.True(testing.GetItemPosition(3) == 1);
        }

        [Fact]
        public void ClearRemovesCorrectlyAllElements()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.Clear();
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
        public void CopiesCorrectlyElementsFromLinkedListToArray()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            int[] arr = new int[testing.Count];
            testing.CopyTo(arr, 0);
            Assert.True(arr[0] == 1);
            Assert.True(arr[1] == 2);
            Assert.True(arr[2] == 3);
        }

        [Fact]
        public void RemovesCorrectlyRequestedItem()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.Remove(2);
            Assert.True(testing.GetItemPosition(2) == -1);
        }

        [Fact]
        public void RemovesCorrectlyLastElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.RemoveLast();
            Assert.True(testing.GetItemPosition(3) == -1);
        }

        [Fact]
        public void RemovesCorrectlyFirstElement()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.RemoveFirst();
            Assert.True(testing.GetItemPosition(1) == -1);
        }

        [Fact]
        public void RemovesCorrectlyItemFromIndicatedPosition()
        {
            var testing = new ArrayLibrary.LinkedCollection<int>();
            testing.Add(1);
            testing.Add(2);
            testing.Add(3);
            testing.RemoveAt(0);
            Assert.True(testing.GetItemPosition(2) == 0);
        }
    }
}
