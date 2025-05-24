using Attention.Window;
using System.Collections.Generic;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class WindowDataContainer
    {
        public WindowAPIData AttentionWindowData { get; private set; }
        public List<WindowAPIData> Windows { get; private set; }
        public WindowDataContainer() {
            AttentionWindowData = new WindowAPIData();
            Windows = new List<WindowAPIData>();
            DI.Register(this);
        }

        public void SetAttentionWindowData(WindowAPIData data)
        {
            AttentionWindowData = data;
        }


    }
}


