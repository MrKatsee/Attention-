using System;

namespace Attention.Main.EventModule
{
    public class ViewEventRouter : EventRouter<IViewEvent>
    {
        public ViewEventRouter(IEventDispatcher<IViewEvent> eventDispatcher) : base(eventDispatcher)
        {

        }

        protected override void OnBeforeHandleEvent(IViewEvent eventData)
        {

        }
    }
}