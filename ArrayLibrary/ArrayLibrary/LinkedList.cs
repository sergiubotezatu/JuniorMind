using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class LinkedCollection<T> : ICollection<T>
    {
        public Node<T> Node;

        public LinkedCollection()
        {
            this.Sentinel = new Node<T>();
            this.Sentinel.NextNode = this.Sentinel;
            this.Sentinel.NextNode.PrevNode = this.Sentinel;
            Node = new Node<T>();
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        private Node<T> Sentinel { get; set; }

        public void Add(T item)
        {
            if (this.Count == 0)
            {
                AddOnlyOne(item);
            }
            else
            {
                AddLast(item);
            }
        }

        public void AddLast(T item)
        {
            this.Node = new Node<T>(item);
            Node.PrevNode = this.Sentinel.PrevNode;
            this.Sentinel.PrevNode = Node;
            Node.NextNode = this.Sentinel;
            Node.PrevNode.NextNode = Node;
            this.Count++;
        }

        public void AddFirst(T item)
        {
            this.Node = new Node<T>(item);
            this.Node.NextNode = this.Sentinel.NextNode;
            this.Sentinel.NextNode.PrevNode = this.Node;
            this.Sentinel.NextNode = this.Node;
            this.Sentinel.PrevNode = this.Sentinel;
            this.Count++;
        }

        public void AddOnlyOne(T item)
        {
            this.Node = new Node<T>(item);
            this.Sentinel.NextNode = Node;
            this.Sentinel.PrevNode = Node;
            this.Sentinel.NextNode.PrevNode = this.Sentinel;
            this.Sentinel.PrevNode.NextNode = this.Sentinel;
            this.Count = 1;
        }

        public void AddBefore(T item, T nodeValue)
        {
            if (Contains(item))
            {
                throw new InvalidOperationException("The node value to be added is already contained in the Linnked List");
            }

            Node<T> toFind = this.Sentinel.NextNode;
            while (toFind != this.Sentinel)
            {
                if (toFind.Value.Equals(nodeValue))
                {
                    Node<T> inserted = new Node<T>(item)
                    {
                        PrevNode = toFind.PrevNode,
                        NextNode = toFind
                    };
                    toFind.PrevNode.NextNode = inserted;
                    toFind.PrevNode = inserted;
                }

                toFind = toFind.NextNode;
            }

            if (Contains(item))
            {
                this.Count++;
            }
        }

        public void AddAfter(T item, T nodeValue)
        {
            if (Contains(item))
            {
                throw new InvalidOperationException("The node value to be added is already contained in the Linnked List");
            }

            Node<T> toFind = this.Sentinel.NextNode;
            while (toFind != this.Sentinel)
            {
                if (toFind.Value.Equals(nodeValue))
                {
                    Node<T> inserted = new Node<T>(item)
                    {
                        PrevNode = toFind,
                        NextNode = toFind.NextNode
                    };
                    toFind.NextNode.PrevNode = inserted;
                    toFind.NextNode = inserted;
                }

                toFind = toFind.NextNode;
            }

            if (Contains(item))
            {
                this.Count++;
            }
        }

        public void Clear()
        {
            this.Sentinel.NextNode = this.Sentinel;
            this.Sentinel.PrevNode = this.Sentinel;
            this.Count = 0;
        }

        public bool Contains(T item)
        {
            if (this.Count <= 0)
            {
                return false;
            }

            return GetItemPosition(item) != -1;
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
                Node<T> element = this.Sentinel.NextNode;
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

            Node<T> element = this.Sentinel.NextNode;
            while (!element.Value.Equals(item))
            {
                element = element.NextNode;
            }

            element.PrevNode.NextNode = element.NextNode;
            element.NextNode.PrevNode = element.PrevNode;
            this.Count--;
            return !Contains(item);
        }

        public void RemoveLast()
        {
            T toBeRemoved = this.Sentinel.PrevNode.Value;
            Remove(toBeRemoved);
        }

        public void RemoveFirst()
        {
            T toBeRemoved = this.Sentinel.NextNode.Value;
            Remove(toBeRemoved);
        }

        public void RemoveAt(int position)
        {
            int count = 0;
            Node<T> toBeRemoved = this.Sentinel;
            if (position >= this.Count)
            {
                throw new InvalidOperationException($"Invalid {nameof(position)}. Number of elements in Linked List is less than your input" +
                    $"or {nameof(position)} was a negative int");
            }

            while (count <= position)
            {
                toBeRemoved = toBeRemoved.NextNode;
                count++;
            }

            Remove(toBeRemoved.Value);
        }

        public int GetItemPosition(T item)
        {
            int count = 0;
            Node<T> toBeFound = this.Sentinel.NextNode;
            while (count < this.Count)
                {
                    if (toBeFound.Value.Equals(item))
                    {
                        return count;
                    }

                    count++;
                    toBeFound = toBeFound.NextNode;
                }

            return -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> element = this.Sentinel.NextNode;
            while (element != this.Sentinel)
            {
                yield return element.Value;
                element = element.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Node<T> element = this.Sentinel.NextNode;
            while (element != this.Sentinel)
            {
                yield return element.Value;
                element = element.NextNode;
            }
        }
    }
}
