using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using System;
using System.Collections.Generic;
using Util;

namespace Attention
{
    public class WindowCaptureLogicEvent : ILogicEvent
    {

    }
    public class WindowSelectViewEvent : IViewEvent
    { 

    }

    public class WindowSelectLogicEvent: ILogicEvent
    {
        private int _id;
        public int id { get; }
        public WindowSelectLogicEvent(int id) { 
            _id = id;
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
        public void SetSelectPanel(WindowSelectViewEvent _event)
        {
            _viewContainer.ActivateView(ViewType.WindowSelect);
            List<Action> actions = new();
            List<WindowAPIData> windows = _windowDataContainer.Windows;
            View.Init(windows,(int i) =>
            {
                _eventQueue.EnqueueLogicEvent(new WindowSelectLogicEvent(i));
            });
           

        }

        public override void OnActivateView()
        {
            View.ResetThumbnails();
        }

    }


}
