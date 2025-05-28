using Attention.Main.EventModule;
using Util;

namespace Attention
{
    public class TaskListViewEvent : IViewEvent { }
}

namespace Attention.View
{
    [DISubscriber]
    public class SettingPanelPresenter : ViewPresenter<UI_SettingPanel>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;

        public SettingPanelPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(
                () => _eventQueue.EnqueueLogicEvent(new WindowCaptureLogicEvent()),
                () => { }, //TODO:manage list of tasks
                () => _viewContainer.DeactivateView(ViewType.SettingPanel));
        }
        
        private void onClickAddTask(WindowCaptureViewEvent _event)
        {
            _viewContainer.ActivateView(ViewType.WindowSelect);
        }
    }

}
