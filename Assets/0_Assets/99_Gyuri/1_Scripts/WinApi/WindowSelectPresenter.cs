using Attention.Data;
using Attention.Main.EventModule;
using Attention.View;
using Attention.Window;
using System;
using System.Collections;
using System.Collections.Generic;
using Util;

namespace Attention
{
    public class WindowSelectViewEvent : IViewEvent
    { 

    }
}

namespace Attention.View
{
    [DISubscriber]
    public class WindowSelectPresenter : ViewPresenter<UI_WIndowSelect>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewContainer))] private IViewLoader _viewContainer;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;

        public WindowSelectPresenter()
        {
            DI.Register(this);
        }
        public void SetSelectPanel(WindowSelectViewEvent _event)
        {
            List<Action> actions = new();
            List<WindowAPIData> windows = _windowDataContainer.Windows;
            foreach(WindowAPIData window in windows) {
                Action action = () =>
                {
                    //로직 이벤트 큐
                };
            }

        }
        public override void OnDeactivateView() {
            View.OnDeactivate();
        }

    }


}
