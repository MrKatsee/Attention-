using Attention.Data;
using Attention.Main.EventModule;
using UnityEngine;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class TimeHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private TimeDataContainer _timeData;

        public TimeHandler()
        {
            DI.Register(this);
        }

        public void CheckProcessTime(DeltaTimeEvent data)
        {
            _timeData.UpdateTime(data.DeltaTime);

            _eventQueue.EnqueueLogicEvent(new CurrentCatStateUpdateEvent(-data.DeltaTime, data.DeltaTime, 0));
            _eventQueue.EnqueueViewEvent(new TimeViewEvent(data.DeltaTime));
        }

        //private void CheckWinFocus()
        //{
        //    if (_timeData._processTime - _lastWinFocusTime > WIN_FOCUS_TIME)
        //    {
        //        _lastWinFocusTime = _timeData._processTime;

        //        //여기에 체크 이벤트 큐 추가
        //    }
        //}
    }
}