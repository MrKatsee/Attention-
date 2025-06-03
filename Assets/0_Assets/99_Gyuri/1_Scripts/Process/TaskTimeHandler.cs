using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using System.Collections.Generic;
using Util;

namespace Attention.Process
{

    [DISubscriber]
    public class TaskTimeHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;
        [Inject(typeof(TaskTimeContainer))] private TaskTimeContainer _taskTimeContainer;
        [Inject(typeof(WindowAPIHandler))] private WindowAPIHandler _windowAPIHandler;
        public TaskTimeHandler()
        {
            DI.Register(this);
        }
        public void CheckTaskTime(DeltaTimeEvent data)
        {
            WindowAPIData window = _windowAPIHandler.GetFocusedWindowData();
            List<string> tasks = _windowDataContainer.Tasks;
            if (tasks.Contains(window.ExePath))
            {
                _taskTimeContainer.UpdateTime(data.DeltaTime);
                _eventQueue.EnqueueViewEvent(new TaskTimerUpdateViewEvent());
            }
        }        
    }

}
