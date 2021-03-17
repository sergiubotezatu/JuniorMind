using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ArrayLibrary;

namespace IntArrayFacts
{
    public class ExceptionTests
    {
        [Fact]
        public void TestCatchException()
        {
            var test = new SortedList<int>(new ArrayLibrary.List<int>());
            test.Add(4);
            test.Add(3);
            test.Add(2);
            test.Add(2);
            string expectedMess = "Parameter can not be greater than list capacity (Parameter 'index')";
            var exception = new Exception();
            try
            {
                test.Insert(5, 0);
            }
            catch(ArgumentOutOfRangeException e)
            {
                exception = e;
            }
            Assert.Equal(expectedMess, exception.Message);
        }

        [Fact]
        public void InsertThrowsArgOutOfRangeIfIndexGreaterThanCapacity()
        {
            var test = new SortedList<int>(new ArrayLibrary.List<int>());
            test.Add(4);
            test.Add(3);
            test.Add(2);
            test.Add(2);
            string expectedMess = "Parameter can not be greater than list capacity (Parameter 'index')";
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => test.Insert(5, 0));
            Assert.True(expectedMess.Equals(exception.Message));
        }

        [Fact]
        public void InsertThrowsArgOutOfRangeIfIndexLessThanZero()
        {
            var test = new SortedList<int>(new ArrayLibrary.List<int>());
            test.Add(4);
            test.Add(3);
            test.Add(2);
            test.Add(2);
            string expectedMess = "Parameter can not be less than 0 (Parameter 'index')";
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => test.Insert(-1, 0));
            Assert.True(expectedMess.Equals(exception.Message));
        }

        [Fact]
        public void RemoveAtThrowsArgOutOfRangeIfIndesLessThanZero()
        {
            var test = new SortedList<int>(new ArrayLibrary.List<int>());
            test.Add(4);
            test.Add(3);
            test.Add(2);
            test.Add(2);
            string expectedMess = "Parameter can not be less than 0 (Parameter 'index')";
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => test.RemoveAt(-1));
            Assert.True(expectedMess.Equals(exception.Message));
        }

        [Fact]
        public void CopyToThrowsArgNullIfDestinationIsNull()
        {
            var test = new SortedList<int>(new ArrayLibrary.List<int>());
            test.Add(4);
            test.Add(3);
            test.Add(2);
            test.Add(2);
            string expectedMess = "Value cannot be null. (Parameter 'array')";
            var exception = Assert.Throws<ArgumentNullException>(() => test.CopyTo(null, 0));
            Assert.True(expectedMess.Equals(exception.Message));
        }

        [Fact]
        public void CopyToThrowsArgOutOFRangeIfIndexLessThanZero()
        {
            var test = new SortedList<int>(new ArrayLibrary.List<int>());
            test.Add(4);
            test.Add(3);
            test.Add(2);
            test.Add(2);
            int[] copyTo = { 1, 2, 3, 4, 5 };
            string expectedMess = "Parameter can not be less than 0 (Parameter 'index')";
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => test.CopyTo(copyTo, -1));
            Assert.True(expectedMess.Equals(exception.Message));
        }

        [Fact]
        public void CopyToThrowsArgumExceptionIfNotEnoughSpaceOnDestination()
        {
            var test = new SortedList<int>(new ArrayLibrary.List<int>());
            test.Add(4);
            test.Add(3);
            test.Add(2);
            test.Add(2);
            int[] copyTo = { 1, 2, 3, 4, 5 };
            string expectedMess =
                "Available space in destination array starting from index is smaller than the source list capacity" +
                "You need minimum 3 more positions after your index";
            var exception = Assert.Throws<ArgumentException>(() => test.CopyTo(copyTo, 2));
            Assert.True(expectedMess.Equals(exception.Message));
        }
    }
}
