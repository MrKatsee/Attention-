using System;
using System.Collections.Generic;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class EntityDataContainer
    {
        private Dictionary<Guid, EntityData> _entityDatas;

        public EntityDataContainer() 
        {
            _entityDatas = new Dictionary<Guid, EntityData>();

            DI.Register(this);
        }

        public void CreateEntityData(Guid id)
        {
            _entityDatas.Add(id, new EntityData());
        }

        public void CreateEntityData(Guid id, bool isCollerable = false)
        {
            _entityDatas.Add(id, new EntityData { isCollerable = isCollerable });
        }

        public void AddEntityData(Guid _id, EntityData data)
        {
            _entityDatas.Add(_id, data);
        }

        public void UpdatetEntityData(Guid _id, EntityData data)
        {
            if (_entityDatas.ContainsKey(_id))
            {
                _entityDatas[_id] = data;
            }
        }

        public EntityData GetEntityData(Guid _id)
        {
            if (_entityDatas.ContainsKey(_id))
            {
                return _entityDatas[_id];
            }

            return null;
        }

        public List<Guid> GetAllEntityIds()
        {
            List<Guid> ids = new List<Guid>();
            foreach(Guid id in _entityDatas.Keys)
            {
                ids.Add(id);
            }
            return ids;
        }

        public void RemoveEntityData(Guid _id)
        {
            if (_entityDatas.ContainsKey(_id))
            {
                _entityDatas.Remove(_id);
            }
        }
    }
}