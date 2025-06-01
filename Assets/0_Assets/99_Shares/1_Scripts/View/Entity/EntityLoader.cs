using System;
using System.Collections.Generic;
using System.Diagnostics;
using Util;
using UnityEngine;

namespace Attention.View
{
    public interface IEntityLoader
    {
        public void CreateEntity(CreateData data);
        public void DeactivateEntity(Guid type);
    }

    public struct CreateData
    {
        public Guid id;
        public EntityType type;
        public string sprite;
        public string animator;
    }

    [DIPublisher]
    public class EntityLoader : IEntityLoader
    {
        private EntityContainer _entityContainer;

        private Queue<CreateData> _createQueue;
        //private Queue<CreateData> _createBindings;
        private Queue<Guid> _deactivateQueue;

        public EntityLoader(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;

            _createQueue = new Queue<CreateData>();
            //_createBindings = new Queue<CreateData>();
            _deactivateQueue = new Queue<Guid>();

            DI.Register(this);
        }

        public void CreateEntity(CreateData data)
        {
            //_createBindings.Enqueue(data);
            _createQueue.Enqueue(data);
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

            //while(_createBindings.Count > 0)
            //{
            //    CreateData data = _createBindings.Dequeue();
            //    UnityEngine.Debug.Log(data);
            //    _entityContainer.CreateAndBindEntity(data.id, data.type);
            //}

            while (_deactivateQueue.Count > 0)
            {
                _entityContainer.DeactivateEntity(_deactivateQueue.Dequeue());
            }
        }
    }
}