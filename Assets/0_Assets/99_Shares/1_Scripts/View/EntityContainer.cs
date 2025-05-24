using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.View
{
    [DIPublisher]
    public class EntityContainer
    {
        private EntityPrefabContainer _entityPrefabContainer;
        private Dictionary<Guid, Entity> _objDict;
        private Dictionary<EntityType, List<Entity>> _entityTypeDict;

        public EntityContainer()
        {

            DI.Register(this);
        }
        
        public void Init()
        {
            _objDict = new Dictionary<Guid, Entity>();
            _entityTypeDict = new Dictionary<EntityType, List<Entity>>();
            _entityPrefabContainer = GameObject.FindAnyObjectByType<EntityPrefabContainer>();
            _entityPrefabContainer.Init();
        }

        public Guid CreateEntity(EntityType type)
        {
            Entity target = _entityPrefabContainer.GetEntity(type);
            if(target == null)
            {
                return Guid.Empty;
            }

            Entity entity = GameObject.Instantiate(target) as Entity;
            entity.Init();

            _objDict.Add(entity.id, entity);
            if (!_entityTypeDict.ContainsKey(entity.type))
            {
                _entityTypeDict[entity.type] = new List<Entity>();
            }

            _entityTypeDict[entity.type].Add(entity);
            
            return entity.id;
        }

        public void UpdateEntityByType(EntityType type)
        {
            if (!_entityTypeDict.ContainsKey(type)) { return; }

            foreach (Entity entity in _entityTypeDict[type])
            {
                entity.UpdateEntity();
            }
        }

        public void UpdateEntity(Guid id)
        {
            if (_objDict.ContainsKey(id))
            {
                _objDict[id].UpdateEntity();
            }
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

            _entityTypeDict[type].Remove(_objDict[id]);
            _objDict.Remove(id);
        }
    }
}