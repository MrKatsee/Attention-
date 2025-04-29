using Attention.Main.EventModule;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Main.InputModule
{
    public class RightMouseInput : IInput
    {
        public bool TryGetInputEvents(out IEnumerable<ILogicEvent> result)
        {
            List<ILogicEvent> eventDatas = new List<ILogicEvent>();

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