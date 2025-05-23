using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class CatDataContainer
    {
        private Dictionary<int, CatData> _catDatas;

        public CatDataContainer()
        {
            _catDatas = new Dictionary<int, CatData>();
        }

        public int CreateCat()
        {
            return RegistCat(new CatData());
        }

        public int RegistCat(CatData data)
        {
            if (data == null)
            {
                return -1;
            }

            int id = _catDatas.Count;
            _catDatas.Add(id, data);
            return id;
        }

        public void LoadData()
        {

        }

        public void SaveData()
        {

        }
    }
}

