using System;
using System.Collections.Generic;
using UnityEngine;
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

        public void SetEnding(Guid id, string ending)
        {
            CatData data = _catDatas[id];
            data.Ending = ending;
            _catDatas[id] = data;
        }

        public void CreateCatData(Guid id, string name)
        {
            CatData data = new CatData(name);
            _catDatas.Add(id, data);
            currentCatId = id;
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
            //Debug.Log(id + " : " + _catDatas.ContainsKey(id));
            //foreach (Guid _id in _catDatas.Keys)
            //{
            //    Debug.Log(_id);
            //}
            if (_catDatas.ContainsKey(id)) { return _catDatas[id]; }
            return new CatData();
        }

        public void SetScore(Guid id, float score)
        {
            if (_catDatas.ContainsKey(id))
            {
                CatData data = _catDatas[id];
                data.score = score;
                _catDatas[id] = data;
            }
        }

        public void SetRemainCoin(Guid id, float coin)
        {
            if (_catDatas.ContainsKey(id))
            {
                CatData data = _catDatas[id];
                data.remainCoin = coin;
                _catDatas[id] = data;
            }
        }

        public List<(Guid, CatData)> GetAllDatas()
        {
            List<(Guid, CatData)> result = new List<(Guid, CatData)>();
            foreach(Guid id in _catDatas.Keys)
            {
                result.Add((id, _catDatas[id]));
            }
            return result;
        }

        public State GetCurrentState()
        {
            if (currentCatId == Guid.Empty)
            {
                return new State();
            }

            CatData catData = _catDatas[currentCatId];
            return new State(catData.Happiness, catData.Bond, catData.Fullness, catData.Cleanliness);
        }

        public void UpdateCatData(Guid id, CatChangeData data)
        {
            if (!_catDatas.ContainsKey(id)) { return; }

            CatData catData = _catDatas[id];

            catData.recordTime += data.RecordTime;

            catData.Fullness += data.Fullness;
            catData.Happiness += data.Happiness;
            catData.Bond += data.Bond;
            catData.Cleanliness += data.Cleanliness;

            _catDatas[id] = catData;

            //UnityEngine.Debug.Log("satiety : " + _catDatas[id].satiety + ", stress : " + _catDatas[id].stress + ", affesction : " + _catDatas[id].affection);
        }

        public void UseItem(Guid id, ItemData data)
        {
            if (!_catDatas.ContainsKey(id)) { return; }

            CatData catData = _catDatas[id];

            catData.Fullness += data.Fullness;
            catData.Happiness += data.Happiness;
            catData.Bond += data.Bond;
            catData.Cleanliness += data.Cleanliness;

            catData.usedCoin += data.Price;
            catData.useItem.Add(data.Name);

            _catDatas[id] = catData;
        }

        public void NextDay(Guid id)
        {
            CatData data = _catDatas[id];
            data.logs.Add(new Log(data.recordTime, data.Happiness, data.Bond, data.Fullness, data.Cleanliness, data.useItem));
            data.recordTime = 0;
            data.useItem = new List<string>();

            _catDatas[id] = data;
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

        public void SaveData()
        {
            List<Guid> ids = new List<Guid>();
            foreach (Guid id in _catDatas.Keys)
            {
                string data = JsonUtility.ToJson(id);
                PlayerPrefs.SetString(id.ToString(), data);
                PlayerPrefs.Save();
                ids.Add(id);
            }

            string json = JsonUtility.ToJson(ids);
            PlayerPrefs.SetString("ID_List", json);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            _catDatas.Clear();

            string json = PlayerPrefs.GetString("ID_List");
            List<Guid> ids = JsonUtility.FromJson<List<Guid>>(json);

            foreach (Guid id in ids)
            {
                string dataJson = PlayerPrefs.GetString(id.ToString());
                CatData data = JsonUtility.FromJson<CatData>(dataJson);
                _catDatas[id] = data;
            }
        }
    }
}

