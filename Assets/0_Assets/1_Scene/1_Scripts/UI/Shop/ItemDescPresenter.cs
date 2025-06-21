using Attention.Main.EventModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class ItemDescPresenter : ViewPresenter<UI_ItemDesc>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;

        public ItemDescPresenter()
        {
            DI.Register(this);
        }

        private void setItemDesc(OnEnterItemEvnet data)
        {
            View.Init(data.ItemData);
        }

    }
}

