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
            this.sentinel = new Node<T>(default)
            {
                List = this
            };
            this.sentinel.NextNode = this.sentinel;
            this.sentinel.PrevNode = this.sentinel;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddLast(item);
        }

        public void Add(Node<T> newNode)
        {
            AddLast(newNode);
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
            AddFirst(new Node<T>(item));
        }

        public void AddFirst(Node<T> newNode)
        {
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
            newNode.List = this;
        }

        public void AddBefore(Node<T> after, T item)
        {
            AddBefore(after, new Node<T>(item));
        }

        public void AddAfter(Node<T> before, Node<T> newNode)
        {
            ThrowNodeIsNull(before);
            AddBefore(before.NextNode, newNode);
        }

        public void AddAfter(Node<T> before, T item)
        {
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
            if (availableSpace < this.Count)
            {
                throw new ArgumentException(
                    "Available space in destination array starting from index is smaller than the source list capacity" +
                    $"You need minimum {this.Count - 1} more positions after your index");
            }

            foreach (T element in this)
            {
                array[arrayIndex] = element;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            Node<T> toFind = Find(item);
            if (toFind == null)
            {
                return false;
            }

            return Remove(toFind);
        }

        public bool Remove(Node<T> selected)
        {
            ThrowNodeIsNull(selected);
            ThrowNodeDoesNotExist(selected);
            selected.PrevNode.NextNode = selected.NextNode;
            selected.NextNode.PrevNode = selected.PrevNode;
            this.Count--;
            return true;
        }

        public void RemoveLast()
        {
            ThrowListIsEmpty();
            Remove(this.sentinel.PrevNode);
        }

        public void RemoveFirst()
        {
            ThrowListIsEmpty();
            Remove(this.sentinel.NextNode);
        }

        public Node<T> Find(T item)
        {
            for (var current = sentinel.NextNode; !current.Equals(this.sentinel); current = current.NextNode)
            {
                if (current.Value.Equals(item))
                {
                    return current;
                }
            }

            return null;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public Node<T> FindLast(T item)
        {
            for (var current = this.sentinel.PrevNode; !current.Equals(this.sentinel); current = current.PrevNode)
            {
                if (current.Value.Equals(item))
                {
                    return current;
                }
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> element = this.sentinel.NextNode; !element.Equals(this.sentinel); element = element.NextNode)
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
            if (node.List == null || !this.Equals(node.List))
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
    }
}
