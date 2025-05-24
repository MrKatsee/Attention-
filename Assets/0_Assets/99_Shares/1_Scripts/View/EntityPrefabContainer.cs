using System.Collections.Generic;
using UnityEngine;

namespace Attention.View
{
    public class EntityPrefabContainer : MonoBehaviour
    {
        [SerializeField] private List<Entity> _entityPrefabList;

        private Dictionary<EntityType, Entity> _entities;

        public EntityPrefabContainer() 
        { 
            _entityPrefabList = new List<Entity>();
        }

        public void Init()
        {
            _entities = new Dictionary<EntityType, Entity>();

            foreach (Entity entity in _entityPrefabList)
            {
                _entities.Add(entity.type, entity);
            }
        }

        public Entity GetEntity(EntityType type)
        {
            if(_entities.ContainsKey(type))
            {
                return _entities[type];
            }

            return null;
        }
    }
}