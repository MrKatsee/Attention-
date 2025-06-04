using System;
using System.Collections.Generic;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class CatDataContainer
    {
        public Guid currentCatId { get; private set; }
        private Dictionary<Guid, CatData> _catDatas;

        public CatDataContainer()
        {
            _catDatas = new Dictionary<Guid, CatData>();

            DI.Register(this);
        }

        public void SetCurrentCat(Guid id)
        {
            if (_catDatas.ContainsKey(id))
            {
                currentCatId = id;
            }
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

        //public CatData GetCatData(Guid id)
        //{
        //    return _catDatas[id];
        //}

        public State GetCurrentState(Guid id)
        {
            CatData catData = _catDatas[id];
            return new State(catData.Happiness, catData.Bond, catData.Fullness, catData.Cleanliness);
        }

        public void UpdateCatData(Guid id, CatChangeData data)
        {
            if (!_catDatas.ContainsKey(id)) { return; }

            CatData catData = _catDatas[id];

            catData.Fullness = data.Fullness;
            catData.Happiness = data.Happiness;
            catData.Bond = data.Bond;
            catData.Cleanliness = data.Cleanliness;

            _catDatas[id] = catData;

            //UnityEngine.Debug.Log("satiety : " + _catDatas[id].satiety + ", stress : " + _catDatas[id].stress + ", affesction : " + _catDatas[id].affection);
        }

        //public List<(Guid, CatData)> GetCatAllDatas()
        //{
        //    List<(Guid, CatData)> dataList = new List<(Guid, CatData)>();
        //    foreach(Guid id in _catDatas.Keys)
        //    {
        //        dataList.Add((id, _catDatas[id]));
        //    }
        //    return dataList;
        //}

        public void ReamoveCatData(Guid id)
        {
            _catDatas.Remove(id);
        }
    }
}

