using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using Util;

namespace Attention.Process
{

    [DISubscriber]
    public class TaskTimeHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;
        [Inject(typeof(WindowAPIHandler))] private TaskTimeContainer _taskTimeContainer;
        [Inject(typeof(WindowAPIHandler))] private WindowAPIHandler _windowAPIHandler;
        public TaskTimeHandler()
        {
            DI.Register(this);
        }

        public void CheckTaskTime()
        {
            WindowAPIData window = _windowAPIHandler.GetFocusedWindowData();

        }



        

        
    }

}
