using Attention.Main.EventModule;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class CatDataContainer
    {
        private Dictionary<Guid, CatEntityData> _catDatas;

        public CatDataContainer()
        {
            _catDatas = new Dictionary<Guid, CatEntityData>();

            DI.Register(this);
        }

        public void CreateCatData(Guid id)
        {
            _catDatas.Add(id, new CatEntityData());
        }

        public void CreateCataData(Guid id, CatEntityData data)
        {
            _catDatas[id] = data;
        }

        public void UpdateAllCat(Action<CatEntityData> atction)
        {
            foreach (Guid id in _catDatas.Keys)
            {
                //UnityEngine.Debug.Log(id);
                atction(_catDatas[id]);
            }
        }

        public CatEntityData GetCatData(Guid id)
        {
            return _catDatas[id];
        }

        public void ReamoveCatData(Guid id)
        {
            _catDatas.Remove(id);
        }
    }
}

