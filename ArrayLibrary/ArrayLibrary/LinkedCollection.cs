using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class LinkedCollection<T> : ICollection<T>
    {
        public Node<T> Node;
        private readonly Node<T> sentinel;

        public LinkedCollection()
        {
            this.sentinel = new Node<T>(default);
            this.sentinel.NextNode = this.sentinel;
            this.sentinel.NextNode.PrevNode = this.sentinel;
            this.Node = new Node<T>(default);
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddLast(item);
        }

        public void Add(Node<T> newNode)
        {
            if (newNode == null)
            {
                throw new ArgumentNullException($"Node {nameof(newNode)} was null. Inserting null node is not allowed.");
            }

            Add(newNode.Value);
        }

        public void AddLast(T item)
        {
                Node<T> toBeAdded = new Node<T>(item);
                Node = this.sentinel;
                AddNewNode(toBeAdded, this.sentinel);
        }

        public void AddLast(Node<T> newNode)
        {
            if (newNode == null)
            {
                throw new ArgumentNullException($"Node {nameof(newNode)} was null. Inserting null node is not allowed.");
            }

            AddLast(newNode.Value);
        }

        public void AddFirst(T item)
        {
            Node<T> toBeAdded = new Node<T>(item);
            Node = this.sentinel.NextNode;
            AddNewNode(toBeAdded, Node);
        }

        public void AddFirst(Node<T> newNode)
        {
            if (newNode == null)
            {
                throw new ArgumentNullException($"Node {nameof(newNode)} was null. Inserting null node is not allowed.");
            }

            AddFirst(newNode.Value);
        }

        public void AddBefore(T item, Node<T> after)
        {
            ThrowItemAlreadyInListException(item);
            if (after == null)
            {
                throw new ArgumentNullException($"Node {nameof(after)} was null. Inserting null node is not allowed.");
            }

            AddBefore(item, after.Value);
        }

        public void AddBefore(T item, T after)
        {
            ThrowItemAlreadyInListException(item);
            Node = Find(after);
            AddNewNode(new Node<T>(item), Node);
        }

        public void AddAfter(T item, Node<T> before)
        {
            ThrowItemAlreadyInListException(item);
            if (before == null)
            {
                throw new ArgumentNullException($"Node {nameof(before)} can not be null");
            }

            AddAfter(item, before.Value);
        }

        public void AddAfter(T item, T before)
        {
            ThrowItemAlreadyInListException(item);
            Node = Find(before).NextNode;
            Node<T> toAdd = new Node<T>(item);
            AddNewNode(toAdd, Node);
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

        public void RemoveLast()
        {
            T toBeRemoved = this.sentinel.PrevNode.Value;
            if (!Remove(toBeRemoved))
            {
                throw new InvalidOperationException("The node you are trying to remove is not " +
                    "contained in this List or List is empty");
            }

            Remove(toBeRemoved);
        }

        public void RemoveFirst()
        {
            T toBeRemoved = this.sentinel.NextNode.Value;
            if (!Remove(toBeRemoved))
            {
                throw new InvalidOperationException("The node you are trying to remove is not " +
                    "contained in this List or List is empty");
            }

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

        public Node<T> FindLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("This list is Empty.Please add a node before trying to access elements.");
            }

            return this.sentinel.PrevNode;
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

        private void ThrowItemAlreadyInListException(T item)
        {
            if (Contains(item))
            {
                throw new InvalidOperationException("The node value to be added is already contained in the Linnked List");
            }
        }

        private void AddNewNode(Node<T> inserted, Node<T> current)
        {
            inserted.NextNode = current;
            inserted.PrevNode = current.PrevNode;
            current.PrevNode.NextNode = inserted;
            current.PrevNode = inserted;
            this.Count++;
        }

        private bool TryFind(T item, out Node<T> toBeFound)
        {
            toBeFound = this.sentinel.NextNode;
            while (toBeFound != this.sentinel)
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
