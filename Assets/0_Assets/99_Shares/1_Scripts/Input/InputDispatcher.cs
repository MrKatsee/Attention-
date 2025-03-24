using Attention.EventModule;
using System.Collections.Generic;

namespace Attention.InputModule
{
    public enum MouseInput
    {
        Left = 0, Right = 1,
    }

    public class InputDispatcher
    {
        private IEventQueue _eventQueue;

        private Dictionary<MouseInput, IInput> _mouseInputs;

        public InputDispatcher(IEventQueue eventQueue)
        {
            _eventQueue = eventQueue;

            _mouseInputs = new Dictionary<MouseInput, IInput>()
            {
                { MouseInput.Left, new LeftMouseInput() },
                { MouseInput.Right, new RightMouseInput() },
            };
        }

        public void Update()
        {
            foreach (IEventData inputEvent in GetMouseInput())
            {
                _eventQueue.EnqueueEvent(inputEvent);
            }
        }

        private IEnumerable<IEventData> GetMouseInput()
        {
            List<IEventData> inputEvents = new List<IEventData>();

            foreach (var input in _mouseInputs.Values)
            {
                if (input.TryGetInputEvents(out IEnumerable<IEventData> eventDatas))
                {
                    inputEvents.AddRange(eventDatas);
                }
            }

            return inputEvents;
        }
    }
}