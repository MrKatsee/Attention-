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
        private EntityPrefabContainer _entityPrefabContainer;

        private Dictionary<Guid, Entity> _objDict;
        private List<Entity> _entityPool;

        public EntityContainer()
        {
            DI.Register(this);
        }
        
        public void Init()
        {
            _objDict = new Dictionary<Guid, Entity>();
            _entityPool = new List<Entity>();
            _entityPrefabContainer = GameObject.FindAnyObjectByType<EntityPrefabContainer>();
            _entityPrefabContainer.Init();
        }

        public void CreateEntity(EntityType type)
        {
            Entity target = _entityPrefabContainer.GetEntity(type);
            if(target == null)
            {
                return;
            }

            Entity entity = GameObject.Instantiate(target) as Entity;
            entity.Init();
            _entityPool.Add(entity);
        }

        public Entity GetEntity(Guid id)
        {
            if (_objDict.ContainsKey(id))
            {
                return _objDict[id];
            }
            return null;
        }

        public void CreateAndBindEntity(Guid id, EntityType type) 
        {
            Entity target = _entityPrefabContainer.GetEntity(type);
            if (target == null)
            {
                return;
            }

            Entity entity = GameObject.Instantiate(target) as Entity;
            entity.Init();
            _objDict[id] = entity;
        }

        public List<Guid> GetIDs()
        {
            List<Guid> ids = new List<Guid>();

            foreach (Guid id in _objDict.Keys)
            {
                ids.Add(id);
            }
            return ids;
        }

        public void DeactivateEntity(Guid id)
        {
            if (_objDict.ContainsKey(id))
            {
                _objDict[id].gameObject.SetActive(false);
            }
        }

        public void RemoveEntity(Guid id)
        {
            if(!_objDict.ContainsKey(id)) { return; }

            EntityType type = _objDict[id].type;

            _entityPool.Add(_objDict[id]);
            _objDict.Remove(id);
        }
    }
}