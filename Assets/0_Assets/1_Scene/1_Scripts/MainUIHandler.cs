using Attention.Data;
using Attention.Main.EventModule;
using Attention.Process;
using Attention.View;
using UnityEngine;
using Util;

namespace Attention
{
    [DISubscriber]
    public class MainUIHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewContainer))] private IViewLoader _viewContainer;

        public MainUIHandler()
        {
            DI.Register(this);
        }

        public void OpenStore(OpenStoreEvent data)
        {
            _viewContainer.ActivateView(ViewType.Shop);
        }

        public void CreateCat(CreateCatEvent _event)
        {
            _viewContainer.ActivateView(ViewType.Cat);
            _eventQueue.EnqueueViewEvent(new MatchCatImageEvent(_event.CatData));
        }
    }
}