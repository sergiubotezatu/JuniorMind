using System;
using System.Collections;
using System.Text;

namespace ArrayLibrary
{
    public class Enumerator : IEnumerator
    {
        private readonly object[] array;
        private readonly int count;

        public Enumerator(object[] arr, int count)
        {
            this.array = arr;
            this.count = count;
        }

        public int Index { get; private set; } = -1;

        public object Current
        {
            get => MoveNext() ? array[Index] : null;
        }

        public bool MoveNext()
        {
            this.Index++;
            return Index < count;
        }

        public void Reset()
        {
            this.Index = -1;
        }
    }
}
