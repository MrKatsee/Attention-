using System.Collections.Generic;
using UnityEngine;

namespace Attention
{
    public interface IEventQueue
    {
        void EnqueueEvent<T>(T eventData) where T : IEvent;
    }

    public interface IEventDispatcher<T> where T : IEvent
    {
        bool TryDispatch(out T eventData);
    }
}

namespace Attention.Main.EventModule
{
    /// <summary>
    /// 이벤트를 받아서, 분배하는 역할을 하는 클래스.
    /// 처리의 체인과 순서 보장(+ 과도한 Call Stack 방지)의 의도를 동시에 충족시키기 위해 2-Stage Queue를 사용한다.
    /// </summary>
    public class EventBus : IEventQueue, IEventDispatcher<ILogicEvent>, IEventDispatcher<IViewEvent>
    {
        private EventQueue<ILogicEvent> _logicEventQueue;
        private EventQueue<IViewEvent> _viewEventQueue;

        public EventBus()
        {
            _logicEventQueue = new EventQueue<ILogicEvent>();
            _viewEventQueue = new EventQueue<IViewEvent>();
        }

        public void EnqueueEvent<T>(T eventData) where T : IEvent
        {
            if (eventData is ILogicEvent logicEvent)
            {
                _logicEventQueue.Enqueue(logicEvent);
            }
            else if (eventData is IViewEvent viewEvent)
            {
                _viewEventQueue.Enqueue(viewEvent);
            }
            else
            {
                Debug.LogError("해당하는 이벤트의 형식을 찾을 수 없음: " + eventData.GetType());
            }
        }

        public bool TryDispatch(out ILogicEvent eventData)
        {
            return TryDispatch(_logicEventQueue, out eventData);
        }
        public bool TryDispatch(out IViewEvent eventData)
        {
            return TryDispatch(_viewEventQueue, out eventData);
        }

        private bool TryDispatch<T>(EventQueue<T> eventQueue, out T eventData) where T : class, IEvent
        {
            if (eventQueue.Count > 0)
            {
                eventData = eventQueue.Dequeue();
                return true;
            }
            else
            {
                eventQueue.Swap();
                if (eventQueue.Count > 0)
                {
                    eventData = eventQueue.Dequeue();
                    return true;
                }
                else
                {
                    eventData = null;
                    return false;
                }
            }
        }
    }
}