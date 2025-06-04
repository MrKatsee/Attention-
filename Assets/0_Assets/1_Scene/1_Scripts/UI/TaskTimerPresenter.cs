using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using Util;

namespace Attention
{
    public class TaskTimerUpdateViewEvent : IViewEvent { }
    
    public class TaskTImerWorkingLogicEvent : ILogicEvent
    {
        public bool IsWorking { get; private set; }
        public TaskTImerWorkingLogicEvent(bool isWorking)
        {
            IsWorking = isWorking;
        }
    }

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

        protected override void OnInitializeView()
        {
            View.Init(
                () => { _eventQueue.EnqueueLogicEvent(new TaskTImerWorkingLogicEvent(true)); },
                () => { _eventQueue.EnqueueLogicEvent(new TaskTImerWorkingLogicEvent(false)); });
            View.SetButton(_taskTimeDataContainer.IsWorking);
        }
        private void OnUpdateTaskTimer(TaskTimerUpdateViewEvent _event)
        {
            View.SetTimer(_taskTimeDataContainer.GetFormattedTime());
            View.SetButton(_taskTimeDataContainer.IsWorking);
        }

    }

}
