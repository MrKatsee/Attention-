using Attention.Main.EventModule;

namespace Attention.Main.EventModule
{
    public interface IEventHandler<T> where T : IEvent
    {
    }
}

namespace Attention.Process
{
    public interface ILogicEventHandler : IEventHandler<ILogicEvent>
    {

    }
}

namespace Attention.View
{
    public interface IViewEventHandler : IEventHandler<IViewEvent>
    {
    }
}