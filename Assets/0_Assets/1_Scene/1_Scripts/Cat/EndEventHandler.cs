using Attention.Data;
using Attention.Main.EventModule;
using System;
using Util;

namespace Attention.Process
{
    public class EndEvent : ILogicEvent { }

    public class EndViewEvent : IViewEvent
    {
        public string ending;
        
        public EndViewEvent(string ending) 
        {
            this.ending = ending;
        }
    }

    [DISubscriber]
    public class EndEventHAndler : ILogicEventHandler
     {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private CatDataContainer _catDataContainer;
        private EndingManager _endinManager;

        public EndEventHAndler()
        {
            _endinManager = new EndingManager();
            _endinManager.resgistEnding("Quene", new QueneEnding());
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

            string ending = _endinManager.GetEnding(data);
            _eventQueue.EnqueueViewEvent(new EndViewEvent(ending));
        }
    }
}