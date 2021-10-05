using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public interface IProduct
    {
        void Notify();
        void AttachAlert(IObserver observer);
    }
}
