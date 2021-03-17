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

        [Fact]
        public void ThrowsNotSupportedExceptionIfReadOnlySortedList()
        {
            var editable = new SortedList<int>(new ArrayLibrary.List<int>())
            {
                4,
                3,
                2,
                1
            };
            var test = new OrderedList<int>(editable);
            string expectedMess =
                "Updating items or capacity of this collection is not allowed." +
                    "It is readOnly";
            var exception1 = Assert.Throws<NotSupportedException>(() => test.Add(2));
            Assert.True(expectedMess.Equals(exception1.Message));
            var exception2 = Assert.Throws<NotSupportedException>(() => test.Insert(1, 2));
            Assert.True(expectedMess.Equals(exception2.Message));
            var exception3 = Assert.Throws<NotSupportedException>(() => test.Remove(2));
            Assert.True(expectedMess.Equals(exception3.Message));
            var exception4 = Assert.Throws<NotSupportedException>(() => test.RemoveAt(1));
            Assert.True(expectedMess.Equals(exception4.Message));
        }
    }
}
