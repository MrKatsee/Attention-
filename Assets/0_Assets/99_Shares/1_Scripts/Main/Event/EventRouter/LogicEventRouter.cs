namespace Attention.Main.EventModule
{
    public class LogicEventRouter : EventRouter<ILogicEvent>
    {
        public LogicEventRouter(IEventDispatcher<ILogicEvent> eventDispatcher, IEventHandlerContainer<ILogicEvent> eventHandlers) : base(eventDispatcher, eventHandlers)
        {
        }
    }
}