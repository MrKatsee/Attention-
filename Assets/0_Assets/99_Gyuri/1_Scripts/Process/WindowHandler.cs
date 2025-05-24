using Attention.Data;
using Attention.Window;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention
{
    public class WindowTransparentLogicEvent : ILogicEvent
    {

    }

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
        public void SetWindowTransparent(WindowTransparentLogicEvent _event)
        {
            Debug.Log("Window 투명화");
            _windowAPIHandler.SetWindowTransparent(_windowDataContainer.AttentionWindowData);
            //_windowAPIHandler.SetWindowToDesktopLayer(_windowDataContainer.AttentionWindowData.HWnd);
            //_windowAPIHandler.PreventZOrderOnClick(_windowDataContainer.AttentionWindowData.HWnd);
        }
        public void GetWindowTransparent(AttentionWindowLogicEvent _event) {
            Debug.Log("Window정보 가져옴");
            Debug.Log(_windowDataContainer == null);
            _windowDataContainer.SetAttentionWindowData(_windowAPIHandler.GetFocusedWindowData());
        }
        
    }

}
