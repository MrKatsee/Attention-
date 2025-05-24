using System;
using System.Collections.Generic;
using Util;

namespace Attention.View
{
    public interface IEntityLoader
    {
        public void CreateEntity(EntityType type);
        public void CreateAndBindEntity(Guid id, EntityType type);
        public void DeactivateEntity(Guid type);
    }

    public struct BindingData
    {
        public Guid id;
        public EntityType type;
    }

    [DIPublisher]
    public class EntityLoader : IEntityLoader
    {
        private EntityContainer _entityContainer;

        private Queue<EntityType> _createQueue;
        private Queue<BindingData> _createBindings;
        private Queue<Guid> _deactivateQueue;

        public EntityLoader(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;

            _createQueue = new Queue<EntityType>();
            _createBindings = new Queue<BindingData>();
            _deactivateQueue = new Queue<Guid>();

            DI.Register(this);
        }

        public void CreateEntity(EntityType type)
        {
            _createQueue.Enqueue(type);
        }

        public void CreateAndBindEntity(Guid id, EntityType type)
        {
            _createBindings.Enqueue(new BindingData { id = id, type = type });
        }

        public void DeactivateEntity(Guid id)
        {
            if (_deactivateQueue.Contains(id)) { return; }

            _deactivateQueue.Enqueue(id);
        }

        public void Update()
        {
            while (_createQueue.Count > 0)
            {
                _entityContainer.CreateEntity(_createQueue.Dequeue());
            }

            while(_createBindings.Count > 0)
            {
                BindingData data = _createBindings.Dequeue();
                _entityContainer.CreateAndBindEntity(data.id, data.type);
            }

            while (_deactivateQueue.Count > 0)
            {
                _entityContainer.DeactivateEntity(_deactivateQueue.Dequeue());
            }
        }
    }
}