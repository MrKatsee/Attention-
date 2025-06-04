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
        private PlayerDataContainer _playerDataContainer;
        private ShopDataContainer _shopDataContainer;
        private TaskTimeContainer _taskDataContainer;
        private EntityDataContainer _entityDataContainer;

        public DataContainer()
        {
            _catDataContainer = new CatDataContainer();
            _timeDataContainer = new TimeDataContainer();
            _windowDataContainer = new WindowDataContainer();
            _playerDataContainer = new PlayerDataContainer();
            _shopDataContainer = new ShopDataContainer();
            _taskDataContainer = new TaskTimeContainer();
            _entityDataContainer = new EntityDataContainer();
        }
    }
}