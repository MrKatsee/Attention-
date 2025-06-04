using Attention.Data;
using Attention.Window;
using Util;

namespace Attention
{
    public class TaskTimerUpdateViewEvent : IViewEvent { }
    
    public class TaskTImerWorkingLogicEvent : ILogicEvent { }

}


namespace Attention.View
{
    [DISubscriber]
    public class TaskTimerPresenter : ViewPresenter<UI_TaskTimer>
    {
        
        [Inject(typeof(TaskTimeContainer))] private TaskTimeContainer _taskTimeContainer;
        public TaskTimerPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(
                () => { },
                () => { });
            View.SetButton(_taskTimeContainer.IsWorking);
        }
        private void OnUpdateTaskTimer(TaskTimerUpdateViewEvent _event)
        {
            View.SetTimer(_taskTimeContainer.GetFormattedTime());
            View.SetButton(_taskTimeContainer.IsWorking);
        }

    }

}
