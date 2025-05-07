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
        public DataContainer()
        {
            InitializeDataContainer();
        }
        private void InitializeDataContainer()
        {
            var dataTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => typeof(IDataContainer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var dataType in dataTypes)
            {
                var dataContainer = Activator.CreateInstance(dataType);
            }
        }
    }
}

