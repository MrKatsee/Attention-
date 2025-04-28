namespace Attention.Main.EventModule
{
    public interface IEventHandler<T> where T : IEvent
    {
    }

    public interface ILogicEventHandler : IEventHandler<ILogicEvent>
    {

    }

    public interface IViewEventHandler : IEventHandler<IViewEvent>
    {

    }
}