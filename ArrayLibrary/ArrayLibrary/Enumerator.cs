using System;
using System.Collections;
using System.Text;

namespace ArrayLibrary
{
    public class Enumerator : IEnumerator
    {
        private readonly ObjectArray array;
        private int index;

        public Enumerator(ObjectArray objectArray)
        {
            this.array = objectArray;
            this.index = -1;
        }

        public object Current
        {
            get => MoveNext() ? array[index] : null;
        }

        public bool MoveNext()
        {
            if (index <= this.array.Count)
            {
                this.index++;
            }

            return index < this.array.Count;
        }

        public void Reset()
        {
            this.index = -1;
        }
    }
}
