using Attention.Data;
using Attention.Window;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention
{


    public class AttentionWindowLogicEvent : ILogicEvent
    {

    }
}

namespace Attention.Process
{
    [DISubscriber]
    public class WindowHandler : ILogicEventHandler
    {
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;
        [Inject(typeof(WindowAPIHandler))] private WindowAPIHandler _windowAPIHandler;
        public WindowHandler() {
            DI.Register(this);
        }

        public void GetWindowTransparent(AttentionWindowLogicEvent _event) {
#if !UNITY_EDITOR
            _windowDataContainer.SetAttentionWindowData(_windowAPIHandler.GetFocusedWindowData());
            _windowAPIHandler.SetWindowTransparent(_windowDataContainer.AttentionWindowData);
#endif
        }

    }

}
