using UnityEngine;

namespace Attention.Main.EventModule
{
    public class TestHandler : ILogicEventHandler
    {
        public void Test(SelectClickEvent data)
        {
            Debug.Log(data.ScreenPosition);
        }
    }
}