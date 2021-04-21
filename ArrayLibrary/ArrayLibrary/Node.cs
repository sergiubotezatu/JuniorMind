using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ArrayLibrary
{
    public class Node<T>
    {
        public Node<T> NextNode;
        public Node<T> PrevNode;
        public T Value;

        public Node(T value)
        {
            this.Value = value;
        }

        public LinkedCollection<T> List
        {
            get
            {
                if (IsLinked())
                {
                    if (this.IsSentinel && this.NextNode.IsSentinel)
                    {
                        return new LinkedCollection<T>();
                    }

                    return CreateList();
                }

                return null;
            }
        }

        internal bool IsSentinel { get; set; }

        public void AddPrevious(Node<T> newNode)
        {
            if (newNode != null)
            {
                if (this.PrevNode != null)
                {
                    newNode.PrevNode = this.PrevNode;
                    this.PrevNode.NextNode = newNode;
                }

                newNode.NextNode = this;
                this.PrevNode = newNode;
            }
        }

        public void AddNext(Node<T> newNode)
        {
            if (newNode != null)
            {
                if (this.NextNode != null)
                {
                    newNode.NextNode = this.NextNode;
                    this.NextNode.PrevNode = newNode;
                }

                this.NextNode = newNode;
                this.NextNode.PrevNode = this;
            }
        }

        private bool IsLinked()
        {
            return PrevNode != null || NextNode != null;
        }

        private LinkedCollection<T> CreateList()
        {
            LinkedCollection<T> toReturn = new LinkedCollection<T>();
            if (IsCircular(out Node<T> toAdd))
            {
                while (!toAdd.IsSentinel)
                {
                    toReturn.Add(toAdd.Value);
                    toAdd = toAdd.NextNode;
                }

                return toReturn;
            }

            while (toAdd != null)
                {
                    toReturn.Add(toAdd.Value);
                    toAdd = toAdd.NextNode;
                }

            return toReturn;
        }

        private bool IsCircular(out Node<T> toCheck)
        {
            toCheck = this;
            while (!toCheck.IsSentinel)
            {
                if (toCheck.PrevNode == null)
                {
                    return false;
                }

                toCheck = toCheck.PrevNode;
            }

            toCheck = toCheck.NextNode;
            return true;
        }
    }
}
