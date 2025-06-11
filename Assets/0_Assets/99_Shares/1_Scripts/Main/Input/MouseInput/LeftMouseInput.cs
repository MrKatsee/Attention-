using Attention.Main.EventModule;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Main.InputModule
{
    public class LeftMouseInput : IInput
    {
        public bool TryGetInputEvents(out IEnumerable<ILogicEvent> result)
        {
            List<ILogicEvent> eventDatas = new List<ILogicEvent>();


            if (Input.GetMouseButtonDown(0))
            {
                eventDatas.Add(new SelectClickEvent(Input.mousePosition));
            }
            else if (Input.GetMouseButtonUp(0))
            {
                eventDatas.Add(new ExitSelectClickEvent(Input.mousePosition));
            }
            else if (Input.GetMouseButton(0))
            {
                eventDatas.Add(new ShiftSelectClickEvent(Input.mousePosition));
            }
            // HS: 확장 가능 (ex) ShitfLeftClickEvent)

            result = eventDatas;
            return eventDatas.Count > 0;
        }
    }
}