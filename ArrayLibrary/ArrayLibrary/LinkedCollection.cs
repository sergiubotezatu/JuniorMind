using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class LinkedCollection<T> : ICollection<T>
    {
        private readonly Node<T> sentinel;

        public LinkedCollection()
        {
            this.sentinel = new Node<T>(default);
            this.sentinel.NextNode = this.sentinel;
            this.sentinel.NextNode.PrevNode = this.sentinel;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddLast(item);
        }

        public void Add(Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            Add(newNode.Value);
        }

        public void AddLast(T item)
        {
             AddBefore(this.sentinel, item);
        }

        public void AddLast(Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            AddLast(newNode.Value);
        }

        public void AddFirst(T item)
        {
            Node<T> toBeAdded = new Node<T>(item);
            AddBefore(this.sentinel.NextNode, toBeAdded);
        }

        public void AddFirst(Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            AddFirst(newNode.Value);
        }

        public void AddBefore(Node<T> after, Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeIsNull(after);
            ThrowNodeBelongsToADifferentList(newNode);
            AddBefore(after, newNode.Value);
        }

        public void AddBefore(Node<T> after, T item)
        {
            ThrowNodeIsNull(after);
            Node<T> newNode = new Node<T>(item)
            {
                NextNode = after,
                PrevNode = after.PrevNode
            };
            after.PrevNode.NextNode = newNode;
            after.PrevNode = newNode;
            this.Count++;
        }

        public void AddAfter(Node<T> before, Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeIsNull(before);
            ThrowNodeBelongsToADifferentList(newNode);
            AddAfter(before, newNode.Value);
        }

        public void AddAfter(Node<T> before, T item)
        {
            ThrowNodeIsNull(before);
            Node<T> toAdd = new Node<T>(item)
            {
                NextNode = before.NextNode,
                PrevNode = before
            };
            before.NextNode.PrevNode = toAdd;
            before.NextNode = toAdd;
            ThrowNodeDoesNotExist(before.Value);
        }

        public void Clear()
        {
            this.sentinel.NextNode = this.sentinel;
            this.sentinel.PrevNode = this.sentinel;
            this.Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Parameter can not be less than 0");
            }

            int availableSpace = array.Length - arrayIndex;
            if (availableSpace >= this.Count)
            {
                Node<T> element = this.sentinel.NextNode;
                for (int i = 0; i < this.Count; i++)
                {
                    array[arrayIndex] = element.Value;
                    element = element.NextNode;
                    arrayIndex++;
                }
            }
            else
            {
                throw new ArgumentException(
                    "Available space in destination array starting from index is smaller than the source list capacity" +
                    $"You need minimum {this.Count - 1} more positions after your index");
            }
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }

            Node<T> toBeRemoved = Find(item);
            toBeRemoved.PrevNode.NextNode = toBeRemoved.NextNode;
            toBeRemoved.NextNode.PrevNode = toBeRemoved.PrevNode;
            this.Count--;
            return !Contains(item);
        }

        public bool Remove(Node<T> selected)
        {
            ThrowNodeIsNull(selected);
            ThrowNodeDoesNotExist(selected.Value);
            return Remove(selected.Value);
        }

        public void RemoveLast()
        {
            T toBeRemoved = this.sentinel.PrevNode.Value;
            ThrowListIsEmpty();
            Remove(toBeRemoved);
        }

        public void RemoveFirst()
        {
            T toBeRemoved = this.sentinel.NextNode.Value;
            ThrowListIsEmpty();
            Remove(toBeRemoved);
        }

        public Node<T> Find(T value)
        {
            if (!TryFind(value, out Node<T> toBeFound))
            {
                string message = this.Count == 0 ?
                    "This list is Empty. Please add a node before trying to access elements."
                    : "The node you are searching for does not exist in this list.";
                throw new InvalidOperationException(message);
            }

            return toBeFound;
        }

        public bool Contains(T item)
        {
            if (this.Count <= 0)
            {
                return false;
            }

            return TryFind(item, out _);
        }

        public Node<T> FindLast(T item)
        {
            Node<T> toReturn = this.sentinel.PrevNode;
            ThrowNodeDoesNotExist(item);
            while (toReturn != this.sentinel)
            {
                if (toReturn.Value.Equals(item))
                {
                    return toReturn;
                }

                toReturn = toReturn.PrevNode;
            }

            return toReturn;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> element = this.sentinel.NextNode;
            while (element != this.sentinel)
            {
                yield return element.Value;
                element = element.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ThrowNodeDoesNotExist(T item)
        {
            if (!TryFind(item, out _))
            {
                throw new InvalidOperationException("Node is not in the current linked list");
            }
        }

        private void ThrowNodeBelongsToADifferentList(Node<T> toCheck)
        {
            if (toCheck.NextNode != null || toCheck.PrevNode != null)
            {
                throw new InvalidOperationException("Node you are trying to insert belongs to a different list.");
            }
        }

        private void ThrowNodeIsNull(Node<T> toCheck)
        {
            if (toCheck == null)
            {
                throw new ArgumentNullException($"Node {nameof(toCheck)} can not be null");
            }
        }

        private void ThrowListIsEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("List is empty. There is no node that can be removed.");
            }
        }

        private bool TryFind(T item, out Node<T> toBeFound)
        {
            toBeFound = this.sentinel.NextNode;
            for (int i = 0; i <= this.Count; i++)
            {
                if (toBeFound.Value.Equals(item))
                {
                    return true;
                }

                toBeFound = toBeFound.NextNode;
            }

            return false;
        }
    }
}
