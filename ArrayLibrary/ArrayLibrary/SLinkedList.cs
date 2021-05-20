using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    class SLinkedList
    {
        private readonly Link pointer;

        public SLinkedList()
        {
            this.pointer = new Link(0);
            this.pointer.Next = new Link(-1);
        }

        public int FirstEmpty
        {
            get
            {
                return this.pointer.Next.Value;
            }
        }

        public void Add(int item)
        {
            Link newItem = new Link(item);
            newItem.Next = this.pointer.Next;
            this.pointer.Next = newItem;
        }

        public void Remove()
        {
            this.pointer.Next = this.pointer.Next.Next;
        }
    }
}
