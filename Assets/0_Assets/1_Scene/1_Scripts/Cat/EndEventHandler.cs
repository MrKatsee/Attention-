using Attention.Data;
using Attention.Main.EventModule;
using Util;

namespace Attention.Process
{
    public class EndEvent : ILogicEvent { }

    public class EndViewEvent : IViewEvent
    {
        string ending;
        
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
            CatData data = _catDataContainer.GetCatData(_catDataContainer.currentCatId);

            string ending = _endinManager.GetEnding(data);
            _eventQueue.EnqueueViewEvent(new EndViewEvent(ending));
        }
    }
}