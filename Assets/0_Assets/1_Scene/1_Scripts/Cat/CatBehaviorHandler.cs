using Attention.Data;
using Attention.Main.EventModule;
using UnityEngine;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class CatBehaviorHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private CatDataContainer _entityDataContainer;

        private float deltaTime;

        public CatBehaviorHandler() 
        {
            DI.Register(this);
        }

        public void OnUpdae(DeltaTimeEvent data)
        {
            //Debug.Log("Time Event Occur!");
            deltaTime = data.DeltaTime;
            _entityDataContainer.UpdateAllCat(UpdateCat);
            _eventQueue.EnqueueViewEvent(new EntityUpdateByTypeEvent(EntityType.Cat));
        }

        //고양이 행동 패턴
        public void UpdateCat(CatEntityData data)
        {
            data.position += new Vector3(1, 0, 0) * (data.direction ? 1 : -1) * deltaTime;

            data.direction = Random.Range(0, 100) > 98 ? !data.direction : data.direction;
        }
    }
}