using Attention.Data;
using Attention.Main.EventModule;
using Attention.View;
using System;
using Util;

namespace Attention.Process
{
    public class CreateEntityEvent : ILogicEvent
    {
        public string EntitySprite;

        public CreateEntityEvent(string entitySprite)
        {
            EntitySprite = entitySprite;
        }
    }

    [DISubscriber]
    public class EntityEventHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(EntityLoader))] private IEntityLoader _entityLoader;
        [Inject] private EntityDataContainer _entityDataContainer;

        public EntityEventHandler()
        {
            DI.Register(this);
        }

        public void EntityCreate(CreateEntityEvent _evnet)
        {
            Guid id = Guid.NewGuid();
            _entityDataContainer.CreateEntityData(id);
            EntityData data = _entityDataContainer.GetEntityData(id);
            data.position = new UnityEngine.Vector3(-21, -7, 0);
            _entityDataContainer.UpdatetEntityData(id, data);
            _entityLoader.CreateEntity(new CreateData { id = id, type = EntityType.Object, sprite = _evnet.EntitySprite });
            _eventQueue.EnqueueViewEvent(new EntityUpdateEvent(id));
        }
    }
}