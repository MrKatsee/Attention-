using Attention.Main.EventModule;
using System.Collections.Generic;

namespace Attention.Main.InputModule
{
    public interface IInput
    {
        bool TryGetInputEvents(out IEnumerable<IEventData> eventDatas);
    }
}