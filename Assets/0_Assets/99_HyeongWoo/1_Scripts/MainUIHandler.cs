using Attention.Main.EventModule;
using Attention.Process;
using Attention.View;
using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Open Store UI!");
            _viewContainer.ActivateView(ViewType.Store);
        }

        public void OnCreateUI(OpenCreateCatUIEvent _event)
        {
            _viewContainer.ActivateView(ViewType.Create_Cat_Panel);
        }

        public void CompleteSceneLoad(CompleteLoadSceneEvent _event)
        {
            _eventQueue.EnqueueLogicEvent(new OpenCreateCatUIEvent());
        }

        public void CreateCat(CreateCatEvent _event)
        {
            _viewContainer.ActivateView(ViewType.Cat);
            _eventQueue.EnqueueViewEvent(new MatchCatImageEvent(_event._catData));
        }
    }
}