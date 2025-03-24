using Attention.EventModule;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.InputModule
{
    public class RightMouseInput : IInput
    {
        public bool TryGetInputEvents(out IEnumerable<IEventData> eventDatas)
        {
            throw new System.NotImplementedException();
        }
    }
}