using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using System.Collections.Generic;
using Util;

namespace Attention
{

    public class WindowSelectLogicEvent: ILogicEvent
    {
        public int Id { get; private set; }
        public WindowSelectLogicEvent(int id) { 
            Id = id;
        }
    }

}

namespace Attention.View
{

    [DISubscriber]
    public class WindowSelectPresenter : ViewPresenter<UI_WIndowSelect>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;

        public WindowSelectPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(() => _viewContainer.DeactivateView(ViewType.WindowSelect));
        }

        public override void OnActivateView()
        {
            View.ResetThumbnails();
            SetSelectPanel();
        }

        private void SetSelectPanel()
        {
            _viewContainer.ActivateView(ViewType.WindowSelect);
            List<WindowAPIData> windows = _windowDataContainer.Windows;

            View.UpdateThumbnails(windows,(int i) =>
            {
                _eventQueue.EnqueueLogicEvent(new WindowSelectLogicEvent(i));
                _viewContainer.DeactivateView(ViewType.WindowSelect);
            });
        }
    }

}
