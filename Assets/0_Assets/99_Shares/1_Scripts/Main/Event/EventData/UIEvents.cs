using UnityEngine;

namespace Attention
{
    public class TestUIEvent : IViewEvent
    {
        public Vector3 ScreenPosition { get; }

        public TestUIEvent(Vector3 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }
}