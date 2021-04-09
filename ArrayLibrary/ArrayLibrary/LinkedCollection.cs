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
            Node<T> newNode = new Node<T>(item);
            Add(newNode);
        }

        public void Add(Node<T> newNode)
        {
            if (this.Count == 0)
            {
                AddFirst(newNode);
            }
            else
            {
                AddLast(newNode);
            }
        }

        public void AddLast(T item)
        {
            AddLast(new Node<T>(item));
        }

        public void AddLast(Node<T> newNode)
        {
            AddBefore(this.sentinel, newNode);
        }

        public void AddFirst(T item)
        {
            Node<T> newNode = new Node<T>(item);
            AddFirst(newNode);
        }

        public void AddFirst(Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeBelongsToADifferentList(newNode);
            AddAfter(this.sentinel, newNode);
        }

        public void AddBefore(Node<T> after, Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeIsNull(after);
            ThrowNodeBelongsToADifferentList(newNode);
            ThrowNodeDoesNotExist(after);
            newNode.NextNode = after;
            newNode.PrevNode = after.PrevNode;
            after.PrevNode.NextNode = newNode;
            after.PrevNode = newNode;
            this.Count++;
        }

        public void AddBefore(Node<T> after, T item)
        {
            AddBefore(after, new Node<T>(item));
        }

        public void AddAfter(Node<T> before, Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeIsNull(before);
            ThrowNodeBelongsToADifferentList(newNode);
            newNode.PrevNode = before;
            newNode.NextNode = before.NextNode;
            before.NextNode.PrevNode = newNode;
            before.NextNode = newNode;
            this.Count++;
        }

        public void AddAfter(Node<T> before, T item)
        {
            ThrowNodeIsNull(before);
            ThrowNodeDoesNotExist(before);
            AddAfter(before, new Node<T>(item));
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
            ThrowNodeDoesNotExist(selected);
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

        public Node<T> Find(T item)
        {
            Node<T> toBeFound = this.sentinel.NextNode;
            foreach (T element in this)
            {
                if (element.Equals(item))
                {
                    return toBeFound;
                }

                GoNext(ref toBeFound);
            }

            return null;
        }

        public bool Contains(T item)
        {
            if (this.Count <= 0)
            {
                return false;
            }

            return Find(item) != null;
        }

        public Node<T> FindLast(T item)
        {
            for (Node<T> toReturn = this.sentinel.PrevNode; !toReturn.Equals(this.sentinel); GetPrev(ref toReturn))
            {
                if (toReturn.Value.Equals(item))
                {
                    return toReturn;
                }
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> element = this.sentinel.NextNode; !element.Equals(this.sentinel); GoNext(ref element))
            {
                yield return element.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ThrowNodeDoesNotExist(Node<T> node)
        {
            if (!TryFind(node))
            {
                throw new InvalidOperationException("Node is not in the current linked list");
            }
        }

        private void ThrowNodeBelongsToADifferentList(Node<T> toCheck)
        {
            if (toCheck.List != null)
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

        private bool TryFind(Node<T> toBeFound)
        {
            T searched = toBeFound.Value;
            Node<T> first = Find(searched);
            Node<T> last = FindLast(searched);
            Node<T> guide = first;
            if (first == null)
            {
                return false;
            }

            if (first == last)
            {
                return toBeFound.Equals(guide);
            }

            while (!guide.Equals(last))
            {
                if (toBeFound.Equals(guide))
                {
                    return true;
                }

                GoNext(ref guide);
            }

            return toBeFound.Equals(guide);
        }

        private void GoNext(ref Node<T> next)
        {
            next = next.NextNode;
        }

        private void GetPrev(ref Node<T> previous)
        {
            previous = previous.PrevNode;
        }
    }
}
