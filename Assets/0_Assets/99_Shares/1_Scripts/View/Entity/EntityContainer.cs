using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.View
{
    interface IEntityContainer
    {
        public Entity GetEntity(Guid id);

        public List<Guid> GetIDs();
    }

    [DIPublisher]
    public class EntityContainer : IEntityContainer
    {
        private EntityResourceContainer _entityPrefabContainer;

        private Dictionary<Guid, Entity> _entityDict;
        //private List<Entity> _entityPool;

        public EntityContainer()
        {
            _entityDict = new Dictionary<Guid, Entity>();
            //_entityPool = new List<Entity>();

            DI.Register(this);
        }
        
        public void Init()
        {
            _entityPrefabContainer = GameObject.FindAnyObjectByType<EntityResourceContainer>();
            _entityPrefabContainer.Init();
        }

        public void CreateEntity(CreateData data)
        {
            Entity target = _entityPrefabContainer.GetEntity();
            Entity entity = GameObject.Instantiate(target) as Entity;
            entity.Init(_entityPrefabContainer.GetSprite(data.sprite));
            _entityDict[data.id] = entity;
        }

        //public void CreateEntity(EntityType type)
        //{
        //    Entity target = _entityPrefabContainer.GetEntity(type);
        //    if(target == null)
        //    {
        //        return;
        //    }

        //    Entity entity = GameObject.Instantiate(target) as Entity;
        //    entity.Init();
        //    _entityPool.Add(entity);
        //}

        public Entity GetEntity(Guid id)
        {
            if (_entityDict.ContainsKey(id))
            {
                return _entityDict[id];
            }
            return null;
        }

        public void CreateAndBindEntity(Guid id, EntityType type) 
        {
            Debug.Log("call");
            Entity target = _entityPrefabContainer.GetEntity(type);
            if (target == null)
            {
                Debug.Log("null...");
                return;
            }

            Entity entity = GameObject.Instantiate(target) as Entity;
            entity.Init();
            _entityDict[id] = entity;
        }

        public List<Guid> GetIDs()
        {
            List<Guid> ids = new List<Guid>();

            foreach (Guid id in _entityDict.Keys)
            {
                ids.Add(id);
            }
            return ids;
        }

        public void DeactivateEntity(Guid id)
        {
            if (_entityDict.ContainsKey(id))
            {
                _entityDict[id].gameObject.SetActive(false);
            }
        }

        public void RemoveEntity(Guid id)
        {
            if(!_entityDict.ContainsKey(id)) { return; }

            //_entityPool.Add(_entityDict[id]);
            _entityDict.Remove(id);
        }
    }
}