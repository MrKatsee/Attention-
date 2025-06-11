using UnityEngine;

namespace Attention
{
    // 좌 클릭
    public class SelectClickEvent : ILogicEvent
    {
        public Vector3 ScreenPosition { get; }

        public SelectClickEvent(Vector3 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }

    public class ShiftSelectClickEvent : ILogicEvent
    {
        public Vector3 ScreenPosition { get; }

        public ShiftSelectClickEvent(Vector3 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }

    public class ExitSelectClickEvent : ILogicEvent
    {
        public Vector3 ScreenPosition { get; }

        public ExitSelectClickEvent(Vector3 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }

    // 우 클릭
    public class InteractClickEvent : ILogicEvent
    {
        public Vector3 ScreenPosition { get; }

        public InteractClickEvent(Vector3 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }
}