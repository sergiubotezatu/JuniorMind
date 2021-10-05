using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class ObservedProduct : IProduct
    {
        public string Name;
        public int Quantity;
        private IObserver observer;

        public ObservedProduct(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }

        public void AttachAlert(IObserver observer)
        {
            this.observer = observer;
        }

        public void Notify()
        {
            observer.Update(this);
        }
    }
}
