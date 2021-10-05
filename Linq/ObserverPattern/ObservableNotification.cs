using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class ObservableNotification
    {
        public ObservedProduct critical;
        public string message;

        public ObservableNotification(ObservedProduct product, string alert)
        {
            critical = product;
            message = alert;
        }
    }
}
