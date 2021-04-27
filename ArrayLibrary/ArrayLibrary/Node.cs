using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ArrayLibrary
{
    public class Node<T>
    {
        public T Value;
        public Node<T> NextNode;
        public Node<T> PrevNode;

        public Node(T value)
        {
            this.Value = value;
        }

        public LinkedCollection<T> List { get; internal set; }
    }
}
