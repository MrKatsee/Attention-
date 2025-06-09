using System;
using System.Numerics;

namespace Attention
{
    public class EntityUpdateEvent : IViewEvent
    {
        public Guid id;

        public EntityUpdateEvent(Guid _id) 
        {
            id = _id;
        }
    }

    public class EntityUpdateByTypeEvent : IViewEvent
    {
        public EntityType type;

        public EntityUpdateByTypeEvent(EntityType type)
        {
            this.type = type;
        }
    }

    public class EntityCreateEvent : IViewEvent
    {
        public Guid id;
        public EntityType type;

        public EntityCreateEvent(Guid _id, EntityType _type)
        {
            this.id = _id;
            type = _type;
        }
    }

    public class EntityRemoveEvent : ILogicEvent
    {
        public Guid id;

        public EntityRemoveEvent(Guid id)
        {
            this.id = id;
        }
    }
}