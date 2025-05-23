using Attention.Window;
using System.Collections.Generic;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class WindowDataContainer
    {
        private List<WindowAPIData> _windows;
        public List<WindowAPIData> Windows
        {
            get { return _windows; }
            set { _windows = value; }
        }
        public WindowDataContainer() {
            _windows = new List<WindowAPIData>();
        }
    }
}


