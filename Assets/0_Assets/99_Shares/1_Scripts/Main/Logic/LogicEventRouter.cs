namespace Attention.Main.EventModule
{
    public class LogicEventRouter : EventRouter<ILogicEvent>
    {
        public LogicEventRouter(IEventDispatcher<ILogicEvent> eventDispatcher) : base(eventDispatcher) { }
    }
}