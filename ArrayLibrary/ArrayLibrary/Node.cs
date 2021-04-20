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
                    return CreateList(this);
                }

                return null;
            }
        }

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

        public Node<T> Start()
        {
            Node<T> head = this;
            while (head.PrevNode != null)
            {
                head = head.PrevNode;
            }

            return head;
        }

        public Node<T> End()
        {
            Node<T> end = this;
            while (end.NextNode != null)
            {
                end = end.NextNode;
            }

            return end;
        }

        private bool IsLinked()
        {
            return PrevNode != null || NextNode != null;
        }

        private LinkedCollection<T> CreateList(Node<T> node)
        {
            LinkedCollection<T> toReturn = new LinkedCollection<T>();
            Node<T> temp = node.PrevNode;
            if (IsCircular(node))
            {
                return new LinkedCollection<T> { this };
            }

            while (node != null)
            {
                toReturn.AddLast(node.Value);
                node = node.NextNode;
            }

            while (temp != null)
            {
                toReturn.AddFirst(temp.Value);
                temp = temp.PrevNode;
            }

            return toReturn;
        }

        private bool IsCircular(Node<T> toCheck)
        {
            Node<T> cycle = toCheck.PrevNode;
            while (!toCheck.Equals(cycle))
            {
                if (toCheck.NextNode == null)
                {
                    return false;
                }

                toCheck = toCheck.NextNode;
            }

            return true;
        }
    }
}
