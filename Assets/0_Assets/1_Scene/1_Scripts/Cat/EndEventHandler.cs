using Attention.Data;
using Attention.Main.EventModule;
using Attention.View;
using System;
using Util;

namespace Attention.Process
{
    public class EndEvent : ILogicEvent { }

    public class EndViewEvent : IViewEvent
    {
        public Guid id;
        
        public EndViewEvent(Guid id) 
        {
            this.id = id;
        }
    }

    [DISubscriber]
    public class EndEventHAndler : ILogicEventHandler
     {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject] private CatDataContainer _catDataContainer;
        [Inject] private PlayerDataContainer _playerDataContainer;
        private EndingManager _endinManager;

        public EndEventHAndler()
        {
            _endinManager = new EndingManager();
            _endinManager.resgistEnding("QueneEnding", new QueneEnding());
            _endinManager.resgistEnding("BadEnding", new BadEnding());
            DI.Register(this);
        }

        public void OnEndEvent(EndEvent _event) 
        {
            Guid id = _catDataContainer.currentCatId;
            if (id == Guid.Empty)
            {
                return;
            }

            CatData data = _catDataContainer.GetCatData(id);

            var ending = _endinManager.GetEnding(data);
            _catDataContainer.SetEnding(id, ending.Item1);
            _catDataContainer.SetScore(id, ending.Item2);
            _catDataContainer.SetRemainCoin(id, _playerDataContainer.GetMoney());

            _viewContainer.DeactivateView(ViewType.Shop);
            _viewContainer.DeactivateView(ViewType.State);
            _eventQueue.EnqueueViewEvent(new EndViewEvent(id));
        }
    }
}