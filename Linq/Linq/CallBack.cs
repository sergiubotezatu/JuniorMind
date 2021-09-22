using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public delegate Notification Notify(Product product, int after);

    public class CallBack
    {
        public Product critical;

        public Notification GetAlert(Notify notification, int sale)
        {
            return notification(critical, sale);
        }
    }

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
