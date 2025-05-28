using Attention.Window;
using System.Collections.Generic;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class WindowDataContainer
    {
        public WindowAPIData AttentionWindowData { get; private set; }
        public List<string> Tasks { get; private set; }
        public List<WindowAPIData> Windows { get; private set; }

        public WindowDataContainer() {
            AttentionWindowData = new WindowAPIData();
            Windows = new List<WindowAPIData>();
            Tasks = new List<string>();
            DI.Register(this);
        }

        public void SetAttentionWindowData(WindowAPIData data)
        {
            AttentionWindowData = data;
        }

        public void SetWindowData(List<WindowAPIData> data)
        {
            Windows = data;
        }

        public void AddTask(WindowAPIData window)
        {
            if(!Tasks.Contains(window.ExePath))
            {
                Tasks.Add(window.ExePath);
            }
        }

        public void DeleteTask(WindowAPIData window)
        {
            Tasks.Remove(window.ExePath);
        }
    }
}


