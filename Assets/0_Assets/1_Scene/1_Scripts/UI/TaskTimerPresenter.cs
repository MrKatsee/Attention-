using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using Util;

namespace Attention
{
    public class TaskTimerUpdateViewEvent : IViewEvent { }
    
    

}


namespace Attention.View
{
    [DISubscriber]
    public class TaskTimerPresenter : ViewPresenter<UI_TaskTimer>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(TaskTimeDataContainer))] private TaskTimeDataContainer _taskTimeDataContainer;
        public TaskTimerPresenter()
        {
            DI.Register(this);
        }


        private void OnUpdateTaskTimer(TaskTimerUpdateViewEvent _event)
        {
            View.SetTimer(_taskTimeDataContainer.GetFormattedTime());
        }

    }

}
