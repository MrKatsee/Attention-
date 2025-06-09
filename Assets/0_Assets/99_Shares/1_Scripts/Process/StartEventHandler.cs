using Attention.Data;
using Attention.Main.EventModule;
using Attention.View;
using System;
using Util;

namespace Attention.Process
{
    public class StartEvent : ILogicEvent { }

    [DISubscriber]
    public class StartEventHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewLoader;
        [Inject] private CatDataContainer _catContainer;
        [Inject] private PlayerDataContainer _playerDataContainer;

        public StartEventHandler()
        {
            DI.Register(this);
        }

        public void OnGameStart(StartEvent _event)
        {
            //기존에 고양이가 있으면 지우기
            if (_catContainer.currentCatId != Guid.Empty)
            {
                _eventQueue.EnqueueLogicEvent(new EntityRemoveEvent(_catContainer.currentCatId));
            }

            _playerDataContainer.SetMoney(1000f);
            _viewLoader.ActivateView(ViewType.CreateCat);
        }
    }
}