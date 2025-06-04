using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using Util;

namespace Attention
{

    public class AttentionWindowLogicEvent : ILogicEvent { }
    public class WindowCaptureLogicEvent : ILogicEvent { }
    public class WindowCaptureViewEvent : IViewEvent { }

}

namespace Attention.Process
{

    [DISubscriber]
    public class WindowHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;
        [Inject(typeof(WindowAPIHandler))] private WindowAPIHandler _windowAPIHandler;

        public WindowHandler() {
            DI.Register(this);
        }

        private void GetWindowTransparent(AttentionWindowLogicEvent _event) {
#if !UNITY_EDITOR
            _windowDataContainer.SetAttentionWindowData(_windowAPIHandler.GetFocusedWindowData());
            _windowAPIHandler.SetWindowTransparent(_windowDataContainer.AttentionWindowData);
#endif
        }

        private void CaptureWindow(WindowCaptureLogicEvent _event)
        {
            _windowDataContainer.SetWindowData(_windowAPIHandler.GetWindowDataList());
            _eventQueue.EnqueueViewEvent(new WindowCaptureViewEvent());

        }
    }

}
