using Attention.Main.EventModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention
{
    [DISubscriber]
    public class Button_Event : MonoBehaviour
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;

        public Button_Event()
        {
            DI.Register(this);
        }

        public void OpenStore()
        {
            Debug.Log("Click Button!");
            _eventQueue.EnqueueLogicEvent(new OpenStoreEvent());
        }

        public void CloseStore()
        {
            _eventQueue.EnqueueViewEvent(new CloseStoreEvent());
        }
    }

}