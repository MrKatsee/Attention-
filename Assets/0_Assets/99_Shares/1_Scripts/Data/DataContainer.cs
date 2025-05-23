using System;
using System.Collections.Generic;
using Util;

namespace Attention.Data
{
    public class DataContainer
    {
        private CatDataContainer _catDataContainer;
        private TimeDataContainer _timeDataContainer;
        private WindowDataContainer _windowDataContainer;

        public DataContainer()
        {
            _catDataContainer = new CatDataContainer();
            _timeDataContainer = new TimeDataContainer();
            _windowDataContainer = new WindowDataContainer();
        }
    }
}