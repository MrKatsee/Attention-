using Attention.Data;
using Attention.Main.EventModule;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class CatBehaviorHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private EntityDataContainer _entityDataContainer;
        [Inject] private GameObjectContainer _objectContainer;

        public CatBehaviorHandler() 
        {
            DI.Register(this);
        }

        public void Update(DeltaTimeEvent data)
        {
            //foreach (var pair in _objectContainer.GetAllData())
            //{
            //    UnityEngine.Debug.Log("[Before] Pos : " + pair.Item2.position + ", Direction : " + pair.Item2.direction);
            //}

            _objectContainer.Update(data.DeltaTime);

            foreach (var pair in _objectContainer.GetAllData())
            {
                //UnityEngine.Debug.Log("[After] Pos : " + pair.Item2.position + ", Direction : " + pair.Item2.direction);
                _entityDataContainer.UpdatetEntityData(pair.Item1, pair.Item2);
                _eventQueue.EnqueueViewEvent(new EntityUpdateEvent(pair.Item1));
            }
        }
    }
}