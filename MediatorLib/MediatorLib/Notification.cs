using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorLib
{
    public class Notification
    {
        public Product Product;
        public string Message;

        public Notification(Product product, string message)
        {
            this.Product = product;
            this.Message = message;
        }
    }
}
