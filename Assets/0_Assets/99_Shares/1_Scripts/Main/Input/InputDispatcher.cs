using Attention.Main.EventModule;
using System.Collections.Generic;

namespace Attention.Main.InputModule
{
    public enum MouseInput
    {
        Left = 0, Right = 1,
    }

    public class InputDispatcher
    {
        private IEventQueue _eventQueue;

        private Dictionary<MouseInput, IInput> _mouseInputs;
        // TODO: 필요하면 KeyboardInput 추가

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
            foreach (IEvent inputEvent in GetMouseInput())
            {
                _eventQueue.EnqueueEvent(inputEvent);
            }
        }

        private IEnumerable<IEvent> GetMouseInput()
        {
            List<IEvent> inputEvents = new List<IEvent>();

            foreach (var input in _mouseInputs.Values)
            {
                if (input.TryGetInputEvents(out IEnumerable<IEvent> eventDatas))
                {
                    inputEvents.AddRange(eventDatas);
                }
            }

            return inputEvents;
        }
    }
}