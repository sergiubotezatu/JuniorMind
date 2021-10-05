using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public interface IObserver
    {
        void Update(IProduct observed);
    }
}
