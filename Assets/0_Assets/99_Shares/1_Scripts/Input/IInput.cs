using Attention.EventModule;
using System.Collections.Generic;

namespace Attention.InputModule
{
    public interface IInput
    {
        bool TryGetInputEvents(out IEnumerable<IEventData> eventDatas);
    }
}