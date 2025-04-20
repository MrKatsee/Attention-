using UnityEngine;

namespace Attention.Main.EventModule
{
    public class TestHandler : IEventHandler
    {
        private void Test(SelectClickEvent data)
        {
            Debug.Log(data.ScreenPosition);
        }
    }
}