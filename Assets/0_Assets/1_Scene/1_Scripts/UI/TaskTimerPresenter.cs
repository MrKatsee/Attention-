using Attention.Data;
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

        [Inject(typeof(TaskTimeContainer))] private TaskTimeContainer _taskTimeContainer;
        public TaskTimerPresenter()
        {
            DI.Register(this);
        }

        public void OnUpdateTaskTimer(TaskTimerUpdateViewEvent _event)
        {
            View.SetTimer(_taskTimeContainer.GetFormattedTime());
        }

    }

}
