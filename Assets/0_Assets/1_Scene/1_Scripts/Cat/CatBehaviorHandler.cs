using Attention.Data;
using Attention.Main.EventModule;
using System;
using UnityEngine;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class CatBehaviorHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private CatDataContainer _dataContainer;

        public CatBehaviorHandler() 
        {
            DI.Register(this);
        }

        public void Update(DeltaTimeEvent data)
        {
            foreach ((Guid, CatEntityData) _dataTuple in _dataContainer.GetCatAllDatas())
            {
                if (_dataTuple.Item2.isActivate)
                {
                    UpdateCat(_dataTuple.Item2, data.DeltaTime);
                    _dataContainer.UpdateCatData(_dataTuple.Item1, _dataTuple.Item2);
                }
            }

            _eventQueue.EnqueueViewEvent(new EntityUpdateByTypeEvent(EntityType.Cat));
        }

        public void UpdateCat(CatEntityData data, float deltaTime)
        {
            data.position += new Vector3(3, 0, 0) * (data.direction ? 1 : -1) * deltaTime;

            data.direction = UnityEngine.Random.Range(0.0f, 100.0f) > 99.9f ? !data.direction : data.direction;
        }
    }
}