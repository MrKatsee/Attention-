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

    public class EntityCreateEvent
    {
        public EntityType type;

        public EntityCreateEvent(EntityType _type)
        {
            type = _type;
        }
    }
}