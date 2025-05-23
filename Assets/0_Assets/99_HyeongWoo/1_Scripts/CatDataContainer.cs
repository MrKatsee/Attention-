using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class CatDataContainer : IDataContainer
    {
        Dictionary<int, CatData> catDatas;

        public CatDataContainer()
        {
            catDatas = new Dictionary<int, CatData>();
        }

        public int CreateCat()
        {
            return registCat(new CatData());
        }

        public int registCat(CatData data)
        {
            if (data == null)
            {
                return -1;
            }

            int id = catDatas.Count;
            catDatas.Add(id, data);
            return id;
        }

        public void loadData()
        {

        }

        public void saveData()
        {

        }
    }
}

