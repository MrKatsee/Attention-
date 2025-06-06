using Attention.Data;
using Attention.Main.EventModule;
using Attention.Process;
using System.Collections.Generic;
using Util;

namespace Attention
{
    public class WindowTaskViewEvent : IViewEvent { }

    public class WindowTaskDeleteLogicEvent : ILogicEvent
    {
        public int Id { get; private set; }
        public WindowTaskDeleteLogicEvent(int id)
        {
            Id = id;
        }
    }
}


namespace Attention.View
{
    [DISubscriber]
    public class SettingPanelPresenter : ViewPresenter<UI_SettingPanel>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;


        public SettingPanelPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(
                () => _eventQueue.EnqueueLogicEvent(new WindowCaptureLogicEvent()),
                () => _viewContainer.DeactivateView(ViewType.SettingPanel),
                () => _eventQueue.EnqueueLogicEvent(new ExitEvent()));
        }

        public override void OnActivateView()
        {
            SetTaskPanel();
        }

        public void SetTaskPanel()
        {
            View.ResetTasks();
            List<string> tasks = _windowDataContainer.Tasks;
            View.UpdateTasks(tasks, (int i) =>
            {
                _eventQueue.EnqueueLogicEvent(new WindowTaskDeleteLogicEvent(i));
            });
        }

        private void OnClickAddTask(WindowCaptureViewEvent _event)
        {
            _viewContainer.ActivateView(ViewType.WindowSelect);
        }

        private void UpdateTaskPanel(WindowTaskViewEvent _event)
        {
            SetTaskPanel();
        }
    }

}
