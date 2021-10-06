using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorLib
{
    public interface ISubscriber
    {
        bool wasTriggered(out Notification notification);
    }
}
