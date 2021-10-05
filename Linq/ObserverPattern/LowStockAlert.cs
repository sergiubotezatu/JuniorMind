using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class LowStockAlert : IObserver
    {
        
        private readonly List<int> thresholds;
        public ObservableNotification notification;

        public bool WasTriggered { get; private set; } = false;


        public LowStockAlert(int critical, int lower, int low)
        {
            this.thresholds = new List<int> { critical, lower, low };
        }

        public void Update(IProduct product)
        {
            ObservedProduct observed = product as ObservedProduct;
            int exceededThreshold = GetExceeded(observed);
            if (exceededThreshold != 0)
            {
                string message = $"Running out of {observed.Name}. Quantity left is below {exceededThreshold}." +
                    $" Products left : {observed.Quantity}";
                this.notification = new ObservableNotification(observed, message);
                WasTriggered = true;
            }            
        }

        private int GetExceeded(ObservedProduct product)
        {
            return this.thresholds.Find(x => x > product.Quantity);
        }
    }
}
