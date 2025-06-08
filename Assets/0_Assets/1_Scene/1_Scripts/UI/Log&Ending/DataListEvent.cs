using System;

namespace Attention
{
    public class DataVIewUpdateEvent : IViewEvent { }

    public class LogViewUpdateEvent : IViewEvent
    {
        public Guid id;

        public LogViewUpdateEvent()
        {
            id = Guid.Empty;
        }

        public LogViewUpdateEvent(Guid id)
        {
            this.id = id;
        }
    }
}