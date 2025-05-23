using Attention.Main.EventModule;
using Attention.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;


namespace Attention.Data
{
    public interface IDataContainer {}

    [DIPublisher]
    public class DataContainer
    {
        List<IDataContainer> _datas;

        public DataContainer()
        {
            DI.Register(this);
            InitializeDataContainer();
        }

        private void InitializeDataContainer()
        {
            _datas = new List<IDataContainer>();
            _datas.Add(new TimeDataContainer());
        }

        public void createContainer(Type type)
        {
            if (typeof(IDataContainer).IsAssignableFrom(type))
            {
                IDataContainer container = (IDataContainer)Activator.CreateInstance(type);
                _datas.Add(container);
            }
        }
    }
}

