using Attention.Main.EventModule;
using Attention.View;
using UnityEngine;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class TestHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewContainer))] private IViewLoader _viewContainer;
        [Inject] private TestDataContainer _testData;

        public TestHandler()
        {
            DI.Register(this);
        }

        public void Test(SelectClickEvent data)
        {
            Debug.Log(data.ScreenPosition);

            _viewContainer.ActivateView(ViewType.Test);
            _eventQueue.EnqueueViewEvent(new TestUIEvent(data.ScreenPosition));
        }
    }
}