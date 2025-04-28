using UnityEngine;
using Util;

namespace Attention.Main.EventModule
{
    [DISubscriber]
    public class TestHandler : ILogicEventHandler
    {
        [Inject] private SceneHandler _sceneHandler;

        public TestHandler()
        {
            DI.Register(this);
        }

        public void Test(SelectClickEvent data)
        {
            Debug.Log(data.ScreenPosition);
            Debug.Log(_sceneHandler);
        }
    }
}