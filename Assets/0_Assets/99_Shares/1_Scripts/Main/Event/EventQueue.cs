using System.Collections.Generic;

namespace Attention.Main.EventModule
{
    public class EventQueue<T> where T : class, IEvent
    {
        private Queue<T> _currentQueue;
        private Queue<T> _nextQueue;

        public int Count => _currentQueue.Count;

        public EventQueue()
        {
            _currentQueue = new Queue<T>();
            _nextQueue = new Queue<T>();
        }

        public void Enqueue(T eventData)
        {
            _currentQueue.Enqueue(eventData);
        }

        public T Dequeue()
        {
            if (_currentQueue.Count > 0)
            {
                return _currentQueue.Dequeue();
            }
            return null;
        }

        // HS: 2-Stage Queue: Call Stack 깊어지는 거 방지 + 처리 순서 보장 
        public void Swap()
        {
            (_currentQueue, _nextQueue) = (_nextQueue, _currentQueue);
            _nextQueue.Clear();
        }
    }
}