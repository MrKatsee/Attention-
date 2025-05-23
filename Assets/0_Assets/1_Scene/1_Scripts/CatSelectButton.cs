using Attention.Main.EventModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Attention
{
    [DISubscriber]
    public class CatSelectButton : ButtonPresenter<UI_Cat_Button>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;

        public CatSelectButton()
        {
            DI.Register(this);
        }

        public override void OnClick()
        {
            _eventQueue.EnqueueLogicEvent(new CreateCatEvent(View.getCatData()));
            _eventQueue.EnqueueViewEvent(new CloseCreateCatUIEvent());
        }
    }
}