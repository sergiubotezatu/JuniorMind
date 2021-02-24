using System;
using System.Collections;
using System.Text;

namespace ArrayLibrary
{
    public class Enumerator : ObjectArray, IEnumerator
    {
        public int Index { get; private set; } = -1;

        public object Current
        {
            get => MoveNext() ? array[Index] : null;
        }

        public bool MoveNext()
        {
            this.Index++;
            return Index < array.Length && Index >= 0;
        }

        public void Reset()
        {
            this.Index = -1;
        }
    }
}
