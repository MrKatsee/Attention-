using System;
using System.Collections.Generic;
using Util;

namespace Attention.View
{
    public interface IEntityLoader
    {
        public void CreateEntity(EntityType type);
        public void UpdateEntityByType(EntityType type);
        public void UpdateEntity(Guid type);
        public void DeactivateEntity(Guid type);
    }

    [DIPublisher]
    public class EntityLoader : IEntityLoader
    {
        private EntityContainer _entityContainer;

        private Queue<EntityType> _createQueue;
        private Queue<EntityType> _updateByTypeQueue;
        private Queue<Guid> _updateQueue;
        private Queue<Guid> _deactivateQueue;

        public EntityLoader(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;

            _createQueue = new Queue<EntityType>();
            _updateByTypeQueue = new Queue<EntityType>();
            _updateQueue = new Queue<Guid>();
            _deactivateQueue = new Queue<Guid>();

            DI.Register(this);
        }

        public void CreateEntity(EntityType type)
        {
            _createQueue.Enqueue(type);
        }

        public void UpdateEntityByType(EntityType type)
        {
            if (_updateByTypeQueue.Contains(type)) { return; }

            _updateByTypeQueue.Enqueue(type);
        }

        public void UpdateEntity(Guid id)
        {
            if (_updateQueue.Contains(id)) { return; }

            _updateQueue.Enqueue(id);
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

            while(_updateByTypeQueue.Count > 0)
            {
                _entityContainer.UpdateEntityByType(_updateByTypeQueue.Dequeue());
            }

            while (_updateQueue.Count > 0)
            {
                _entityContainer.UpdateEntity(_updateQueue.Dequeue());
            }

            while (_deactivateQueue.Count > 0)
            {
                _entityContainer.DeactivateEntity(_deactivateQueue.Dequeue());
            }
        }
    }
}