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
        private TaskTimeDataContainer _taskTimeDataContainer;
        private EntityDataContainer _entityDataContainer;
        private GameObjectContainer _gameObjectContainer;
        private BehaviorContainer _behaviorContainer;

        public DataContainer()
        {
            _catDataContainer = new CatDataContainer();
            _timeDataContainer = new TimeDataContainer();
            _windowDataContainer = new WindowDataContainer();
            _playerDataContainer = new PlayerDataContainer();
            _shopDataContainer = new ShopDataContainer();
            _taskTimeDataContainer = new TaskTimeDataContainer();
            _entityDataContainer = new EntityDataContainer();
            _gameObjectContainer = new GameObjectContainer();
            _behaviorContainer = new BehaviorContainer();
        }
    }
}