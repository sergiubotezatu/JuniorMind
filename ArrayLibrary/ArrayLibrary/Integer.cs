using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class IntNumber
    {
        private readonly int integer;

        public IntNumber(int value)
        {
            this.integer = value;
        }

        public int GetValue()
        {
            return this.integer;
        }

        public bool IsEqualTo(IntNumber secondInt)
        {
            if (secondInt == null)
            {
                return false;
            }

            return this.integer == secondInt.GetValue();
        }
    }
}
