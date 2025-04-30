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
            foreach (ILogicEvent inputEvent in GetMouseInput())
            {
                _eventQueue.EnqueueLogicEvent(inputEvent);
            }
        }

        private IEnumerable<ILogicEvent> GetMouseInput()
        {
            List<ILogicEvent> inputEvents = new List<ILogicEvent>();

            foreach (var input in _mouseInputs.Values)
            {
                if (input.TryGetInputEvents(out IEnumerable<ILogicEvent> eventDatas))
                {
                    inputEvents.AddRange(eventDatas);
                }
            }

            return inputEvents;
        }
    }
}