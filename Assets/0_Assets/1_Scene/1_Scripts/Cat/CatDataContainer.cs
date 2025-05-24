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
            CatEntityData data = new CatEntityData();
            _catDatas.Add(id, data);
        }

        public void CreateCataData(Guid id, CatEntityData data)
        {
            _catDatas[id] = data;
        }

        //public void UpdateAllCat(Action<CatEntityData> atction)
        //{
        //    foreach (Guid id in _catDatas.Keys)
        //    {
        //        atction(_catDatas[id]);
        //    }
        //}

        public CatEntityData GetCatData(Guid id)
        {
            return _catDatas[id];
        }

        public void UpdateCatData(Guid id, CatEntityData data)
        {
            if (!_catDatas.ContainsKey(id)) { return; }

            _catDatas[id] = data;
        }

        public List<(Guid, CatEntityData)> GetCatAllDatas()
        {
            List<(Guid, CatEntityData)> dataList = new List<(Guid, CatEntityData)>();
            foreach(Guid id in _catDatas.Keys)
            {
                dataList.Add((id, _catDatas[id]));
            }
            return dataList;
        }

        public void ReamoveCatData(Guid id)
        {
            _catDatas.Remove(id);
        }
    }
}

