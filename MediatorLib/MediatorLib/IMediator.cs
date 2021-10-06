using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorLib
{
    public interface IMediator
    {
        void Subscribe(ISubscriber subscriber);
        void Notify(ISubscriber subscriber);
    }
}
