using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class Product
    {
        public string Name;

        public Product(string name)
        {
            this.Name = name;
        }

        public int Quantity { get; internal set; }

        public int SelectedQty
        {
            internal get => SelectedQty;
            set
            {
                if (value > Quantity)
                {
                    throw new InvalidOperationException("The quantity requested is greater than the current available quantity");
                }
                SelectedQty = value;
            }
        }         
    }
}
