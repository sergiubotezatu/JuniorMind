using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public sealed class Node<T>
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
                    LinkedCollection<T> reference = new LinkedCollection<T>
                    {
                        this
                    };
                    return reference;
                }

                return null;
            }
        }

        public void AddNewNode(T item)
        {
            Node<T> inserted = new Node<T>(item);
            inserted.NextNode = this;
            inserted.PrevNode = this.PrevNode;
            this.PrevNode.NextNode = inserted;
            this.PrevNode = inserted;
        }

        private bool IsLinked()
        {
            return this.NextNode != null || this.PrevNode != null;
        }
    }
}
