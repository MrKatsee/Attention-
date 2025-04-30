using System;

namespace Attention.Main.EventModule
{
    public class ViewEventRouter : EventRouter<IViewEvent>
    {
        public ViewEventRouter(IEventDispatcher<IViewEvent> eventDispatcher, IEventHandlerContainer<IViewEvent> eventHandlers) : base(eventDispatcher, eventHandlers)
        {
        }
    }
}