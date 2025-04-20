using Attention.Main.EventModule;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Main.InputModule
{
    public class LeftMouseInput : IInput
    {
        public bool TryGetInputEvents(out IEnumerable<IEventData> eventDatas)
        {
            if (Input.GetMouseButtonDown(0))
            {
                eventDatas = GetEvents();
                return true;
            }

            eventDatas = null;
            return false;
        }

        private IEnumerable<IEventData> GetEvents()
        {
            List<IEventData> eventDatas = new List<IEventData>();


            return eventDatas;
        }
    }
}