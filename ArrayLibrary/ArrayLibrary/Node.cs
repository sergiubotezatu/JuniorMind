using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class Node<T>
    {
        public Node<T> NextNode;
        public Node<T> PrevNode;
        public T Value;

        public Node()
        {
        }

        public Node(T value)
        {
            this.Value = value;
        }
    }
}
