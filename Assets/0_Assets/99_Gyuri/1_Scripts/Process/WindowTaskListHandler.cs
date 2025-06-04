using Attention.Data;
using Attention.Main.EventModule;
using Util;

namespace Attention.Process
{

    [DISubscriber]
    public class WindowTaskListHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;

        public WindowTaskListHandler()
        {
            DI.Register(this);
        }

        private void AddTask(WindowSelectLogicEvent data)
        {
            _windowDataContainer.AddTask(data.Id);
            _eventQueue.EnqueueViewEvent(new WindowTaskViewEvent());
        }

        private void DeleteTask(WindowTaskDeleteLogicEvent data)
        {
            _windowDataContainer.DeleteTask(data.Id);
            _eventQueue.EnqueueViewEvent(new WindowTaskViewEvent());
        }

    }

}

