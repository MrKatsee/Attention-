using Attention.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Util;

namespace Attention
{
    [DIPublisher]
    public class GameObjectContainer
    {
        Dictionary<Guid, IGameObject> objDict;

        public GameObjectContainer()
        {
            objDict = new Dictionary<Guid, IGameObject>();

            DI.Register(this);
        }

        public List<(Guid, EntityData)> GetAllData()
        {
            List<(Guid, EntityData)> list = new List<(Guid, EntityData)>();
            foreach (IGameObject value in objDict.Values) 
            {
                list.Add(value.GetEntityData());
            }

            return list;
        }

        public void Register(Guid id, IGameObject obj)
        {
            UnityEngine.Debug.Log("Regist : " + id);
            objDict[id] = obj;
        }

        public void Update(float deltaTime)
        {
            foreach (IGameObject obj in objDict.Values)
            {
                obj.Update(deltaTime);
            }
            //Collision
        }

        public void Remove(Guid id)
        {
            objDict.Remove(id);
        }
    }
}