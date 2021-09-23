using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class Notification
    {
        public Product critical;
        public string message;

        public Notification(Product product, string alert)
        {
            critical = product;
            message = alert;
        }
    }
}
