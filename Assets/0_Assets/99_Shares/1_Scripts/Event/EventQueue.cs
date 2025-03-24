using System.Collections.Generic;

namespace Attention.EventModule
{
    public interface IEventQueue
    {
        void EnqueueEvent(IEventData eventData);
    }

    public class EventQueue : IEventQueue
    {
        public void Update()
        {

        }

        public void EnqueueEvent(IEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}