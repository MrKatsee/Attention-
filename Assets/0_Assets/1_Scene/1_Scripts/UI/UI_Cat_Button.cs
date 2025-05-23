using Attention.Main.EventModule;
using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Attention
{
    [DISubscriber]
    public class UI_Cat_Button : UI_Button
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        public override ViewType Type => ViewType.Create_Cat_Panel;

        [SerializeField] private InputField _catData;

        public UI_Cat_Button()
        {
            DI.Register(this);
        }

        public string getCatData()
        {
            return _catData.text;
        }

        public void OnClick()
        {
            _eventQueue.EnqueueLogicEvent(new CreateCatEvent(getCatData()));
            _eventQueue.EnqueueViewEvent(new CloseCreateCatUIEvent());
        }
    }
}