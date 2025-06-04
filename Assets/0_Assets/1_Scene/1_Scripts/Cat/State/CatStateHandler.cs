using Attention.Data;
using Attention.Main.EventModule;
using Util;
using UnityEngine;
using System;

namespace Attention.Process
{
    [DISubscriber]
    public class CatStateHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private CatDataContainer _dataContainer;

        public CatStateHandler()
        {
            DI.Register(this);
        }

        public void OnStateUpdate(CatStateUpdateEvent data)
        {
            //CatData catData = _dataContainer.GetCatData(data.id);

            _dataContainer.UpdateCatData(data.id, new CatChangeData(data.satiety, data.stress, data.affection));
            _eventQueue.EnqueueViewEvent(new CatStateViewEvent(data.id, data.satiety, data.stress, data.affection));
        }

        public void Update(CurrentCatStateUpdateEvent data)
        {
            Guid id = _dataContainer.currentCatId;
            if (id == Guid.Empty)
            {
                return;
            }

            _dataContainer.UpdateCatData(id, new CatChangeData(data.satiety, data.stress, data.affection));
            _eventQueue.EnqueueViewEvent(new CatStateViewEvent(id, data.satiety, data.stress, data.affection));
        }
    }
}