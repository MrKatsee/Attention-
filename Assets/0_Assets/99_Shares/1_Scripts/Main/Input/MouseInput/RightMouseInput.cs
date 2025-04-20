using Attention.Main.EventModule;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Main.InputModule
{
    public class RightMouseInput : IInput
    {
        public bool TryGetInputEvents(out IEnumerable<IEventData> eventDatas)
        {
            throw new System.NotImplementedException();
        }
    }
}