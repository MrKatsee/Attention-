using Attention.Main.EventModule;
using Attention.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Attention.Data
{
    public interface IDataContainer
    {
    }


    public class DataContainer
    {
        List<IDataContainer> _datas;
        public DataContainer()
        {
            _datas = new List<IDataContainer>();
            InitializeDataContainer();
        }
        private void InitializeDataContainer()
        {
            _datas.Add(new TimeDataContainer());
            //_datas.Add(new TestDataContainer());
            
        }
    }
}

