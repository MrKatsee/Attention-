using System;
using System.Collections.Generic;
using System.Diagnostics;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class CatDataContainer
    {
        private Dictionary<Guid, CatData> _catDatas;

        public CatDataContainer()
        {
            _catDatas = new Dictionary<Guid, CatData>();

            DI.Register(this);
        }

        public void CreateCatData(Guid id)
        {
            CatData data = new CatData();
            _catDatas.Add(id, data);
        }

        public void CreateCataData(Guid id, CatData data)
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

        public CatData GetCatData(Guid id)
        {
            return _catDatas[id];
        }

        public void UpdateCatData(Guid id, CatData data)
        {
            if (!_catDatas.ContainsKey(id)) { return; }

            _catDatas[id] = data;
        }

        public List<(Guid, CatData)> GetCatAllDatas()
        {
            List<(Guid, CatData)> dataList = new List<(Guid, CatData)>();
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

