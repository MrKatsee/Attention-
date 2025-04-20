using Attention.Main.EventModule;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Main.InputModule
{
    public class RightMouseInput : IInput
    {
        public bool TryGetInputEvents(out IEnumerable<IEvent> result)
        {
            List<IEvent> eventDatas = new List<IEvent>();

            if (Input.GetMouseButtonDown(1))
            {
                eventDatas.Add(new InteractClickEvent(Input.mousePosition));
            }
            // HS: 확장 가능 (ex) ShitfLeftClickEvent)

            result = eventDatas;
            return eventDatas.Count > 0;
        }
    }
}