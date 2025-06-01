using Attention.Data;
using Attention.Main.EventModule;
using System;
using UnityEngine;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class CatBehaviorHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private EntityDataContainer _entityDataContainer;

        public CatBehaviorHandler() 
        {
            DI.Register(this);
        }

        public void Update(DeltaTimeEvent data)
        {
            foreach (Guid id in _entityDataContainer.GetAllEntityIds())
            {
                EntityData entityData = _entityDataContainer.GetEntityData(id);
                if(entityData.isActivate)
                {
                    UpdateCat(entityData, data.DeltaTime);
                    _entityDataContainer.UpdatetEntityData(id, entityData);
                    _eventQueue.EnqueueViewEvent(new EntityUpdateEvent(id));
                }
            }

            _eventQueue.EnqueueViewEvent(new EntityUpdateByTypeEvent(EntityType.Cat));
        }

        public void UpdateCat(EntityData data, float deltaTime)
        {
            data.position += new Vector3(3, 0, 0) * (data.direction ? 1 : -1) * deltaTime;

            data.direction = UnityEngine.Random.Range(0.0f, 100.0f) > 99.9f ? !data.direction : data.direction;
        }
    }
}