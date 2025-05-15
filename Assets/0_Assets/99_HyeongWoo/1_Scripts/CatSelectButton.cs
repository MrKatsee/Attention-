using Attention.Main.EventModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Attention
{
    [DISubscriber]
    public class CatSelectButton : MonoBehaviour
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;

        [SerializeField] private Text _catData;

        public CatSelectButton()
        {
            DI.Register(this);
        }

        public void CreateCat()
        {
            _eventQueue.EnqueueLogicEvent(new CreateCatEvent(_catData.text));
            _eventQueue.EnqueueViewEvent(new CloseCreateCatUIEvent());
        }
    }
}