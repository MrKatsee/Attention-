using System;

namespace Attention
{
    public class DataVIewUpdateEvent : IViewEvent { }

    public class LogViewUpdateEvent : IViewEvent
    {
        public Guid id;

        public LogViewUpdateEvent(Guid id)
        {
            this.id = id;
        }
    }
}