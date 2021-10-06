using System;
using System.Collections.Generic;

namespace MediatorLib
{
    public class Product : ISubscriber
    {
        public string Name;
        public int Quantity;
        Notification notification;


        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public bool wasTriggered(out Notification notification)
        {
            notification = this.notification;
            return this.notification != null;
        }

        public void SubscribeTo(Mediator mediator)
        {
            mediator.Subscribe(this);
        }

        public void SellProduct(int bought)
        {
            int remaining = Quantity - bought;
            int exceeded = ExceededThreshold(Quantity, remaining);
            Quantity = remaining;
            if (exceeded != 0)
            {                
                string message = $"Running out of {this.Name}. Quantity left is below {exceeded}." +
                  $" Products left : {this.Quantity}";
                this.notification = new Notification(this, message);
            }            
        }

        private int ExceededThreshold(int initialQty, int currentQty)
        {
            List<int> thresholds = new List<int>() { 2, 5, 10 };
            int exceeded = thresholds.Find(x => initialQty >= x && currentQty < x);
            return exceeded;
        }
    }
}
