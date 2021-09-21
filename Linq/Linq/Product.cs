using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class Product
    {
        public string Name;
        public int Quantity;

        public Product(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }
        
        public delegate string Notification(Product product, int after);

        public string GetWarningMessage(Notification notification, int sale)
        {
            return notification(this, sale);          
        }
    }   
}
