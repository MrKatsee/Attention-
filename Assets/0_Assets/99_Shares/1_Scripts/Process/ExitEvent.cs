using UnityEngine;
using Util;

namespace Attention.Process
{
    public class ExitEvent : ILogicEvent { }

    [DISubscriber]
    public class ExitEventHandler : ILogicEventHandler 
    {
        public ExitEventHandler() 
        {
            DI.Register(this);
        }

        public void OnExitEvent(ExitEvent _event)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // 어플리케이션 종료
#endif
        }
    }
}