using Attention.Data;
using Attention.Main.EventModule;
using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.Process
{
    public class TimeHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private TimeDataContainer _timeData;

        private const float WIN_FOCUS_TIME = 2.0f;
        //FocusedWindow
        private float _lastWinFocusTime;
        public TimeHandler()
        {
            _lastWinFocusTime = 0f;
            DI.Register(this);
        }

        public void CheckProcessTime(TimeEvent data)
        {
            _timeData.ProcessTime += data.DeltaTime;
            CheckWinFocus();


        }

        private void CheckWinFocus()
        {
            if (_timeData.ProcessTime - _lastWinFocusTime > WIN_FOCUS_TIME)
            {
                _lastWinFocusTime = _timeData.ProcessTime;
                //여기에 체크 이벤트 큐 추가

            }
        }

    }
}

