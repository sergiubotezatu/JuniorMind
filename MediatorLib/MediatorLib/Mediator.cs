using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MediatorLib
{
    public class Mediator : IMediator
    {
        private List<ISubscriber> subscribers;

        public Mediator()
        {
            subscribers = new List<ISubscriber>();
        }

        public void Notify(ISubscriber subscriber)
        {
            if (subscriber.wasTriggered(out Notification alert))
            {
                GetAlert(alert);
            }
        }

        public void Subscribe(ISubscriber subscriber)
        {
            this.subscribers.Add(subscriber);
        }

        public Action<Notification> GetAlert;

        public bool NotificationsStatus()
        {
            return subscribers.Any(x => x.wasTriggered(out _));
        }
    }
}
