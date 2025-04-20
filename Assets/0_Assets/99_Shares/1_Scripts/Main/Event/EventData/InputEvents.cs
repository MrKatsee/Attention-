using Attention.Main.EventModule;
using UnityEngine;

namespace Attention
{
    public class LeftClickEvent : ILogicEvent
    {
        public Vector3 ScreenPosition { get; }

        public LeftClickEvent(Vector3 screenPosition)
        {
            ScreenPosition = screenPosition;
        }
    }
}