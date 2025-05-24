using System;
using System.Collections.Generic;
using UnityEngine;
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

        public void AddEntityData(Guid _id, EntityData data)
        {
            _entityDatas.Add(_id, data);
        }

        public void SetEntityData(Guid _id, EntityData data)
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

        public void RemoveEntityData(Guid _id)
        {
            if (_entityDatas.ContainsKey(_id))
            {
                _entityDatas.Remove(_id);
            }
        }
    }

    public class EntityData
    {
        public Vector3 position;
        public bool direction;

        public bool isActivate;
        public bool isVisible;
    }
}