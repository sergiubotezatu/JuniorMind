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
            Node<T> newNode = new Node<T>(item)
            {
                NextNode = this.sentinel,
                PrevNode = this.sentinel.PrevNode
            };
            this.sentinel.PrevNode.NextNode = newNode;
            this.sentinel.PrevNode = newNode;
            this.Count++;
        }

        public void AddLast(Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeBelongsToADifferentList(newNode);
            this.sentinel.AddPrevious(newNode);
            this.Count++;
        }

        public void AddFirst(T item)
        {
            Node<T> newNode = new Node<T>(item);
            newNode.NextNode = this.sentinel.NextNode;
            newNode.PrevNode = this.sentinel;
            this.sentinel.NextNode.PrevNode = newNode;
            this.sentinel.NextNode = newNode;
            this.Count++;
        }

        public void AddFirst(Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeBelongsToADifferentList(newNode);
            this.sentinel.AddNext(newNode);
            this.Count++;
        }

        public void AddBefore(Node<T> after, Node<T> newNode)
        {
            ThrowNodeIsNull(newNode);
            ThrowNodeIsNull(after);
            ThrowNodeBelongsToADifferentList(newNode);
            ThrowNodeDoesNotExist(after);
            after.AddPrevious(newNode);
            IncludeInList(after);
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
            ThrowNodeDoesNotExist(before);
            before.AddNext(newNode);
            IncludeInList(before);
            this.Count++;
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
            IncludeInList(toBeRemoved);
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
            ThrowListIsEmpty();
            this.sentinel.PrevNode = this.sentinel.PrevNode.PrevNode;
            this.sentinel.PrevNode.NextNode = this.sentinel;
            this.Count--;
        }

        public void RemoveFirst()
        {
            ThrowListIsEmpty();
            this.sentinel.NextNode = this.sentinel.NextNode.NextNode;
            this.sentinel.NextNode.PrevNode = this.sentinel;
            this.Count--;
        }

        public Node<T> Find(T item)
        {
            Node<T> foundNode = UnLoopNode();
            for (int i = 0; i < this.Count; i++)
            {
                if (foundNode.Value.Equals(item))
                {
                    return foundNode;
                }

                GoNext(ref foundNode);
            }

            return null;
        }

        public bool Contains(T item)
        {
            foreach (T element in this)
            {
                if (element.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public Node<T> FindLast(T item)
        {
            Node<T> foundNode = UnLoopNode().End();
            for (int i = 0; i < this.Count; i++)
            {
                if (foundNode.Value.Equals(item))
                {
                    return foundNode;
                }

                GetPrev(ref foundNode);
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

        private void IncludeInList(Node<T> toAdd)
        {
            Node<T> start = toAdd.Start();
            Node<T> end = toAdd.End();
            this.sentinel.NextNode = start;
            this.sentinel.PrevNode = end;
            start.PrevNode = this.sentinel;
            end.NextNode = this.sentinel;
        }

        private Node<T> UnLoopNode()
        {
            Node<T> looped = this.sentinel.NextNode;
            Node<T> unlooped = new Node<T>(looped.Value);
            GoNext(ref looped);
            while (!looped.Equals(this.sentinel))
            {
                unlooped.AddNext(new Node<T>(looped.Value));
                GoNext(ref looped);
                GoNext(ref unlooped);
            }

            return unlooped.Start();
        }

        private void ThrowNodeDoesNotExist(Node<T> node)
        {
            if (!node.List.IsEqualTo(this))
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

        private void GoNext(ref Node<T> next)
        {
            if (next != null)
            {
                next = next.NextNode;
            }
        }

        private void GetPrev(ref Node<T> previous)
        {
            if (previous != null)
            {
                previous = previous.PrevNode;
            }
        }

        private bool IsEqualTo(LinkedCollection<T> list)
        {
            Node<T> comparer = this.sentinel.NextNode;
            if (list.Count != this.Count)
            {
                return false;
            }

            foreach (T element in list)
            {
                if (!comparer.Value.Equals(element))
                {
                    return false;
                }

                comparer = comparer.NextNode;
            }

            return true;
        }
    }
}
