using Attention.Data;
using Attention.Main.EventModule;
using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class ShopPresenter : ViewPresenter<UI_Shop>
    {
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;

        [Inject] private ShopDataContainer _shopDataContainer;

        public ShopPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnExitClick, OnClickBuy);
            View.SetItemDatas(_shopDataContainer.GetAllItemDatas());
        }

        private void OnExitClick()
        {
            _viewContainer.DeactivateView(ViewType.Shop);
        }

        private void OnClickBuy(int id)
        {
            _eventQueue.EnqueueLogicEvent(new BuyItemEvent(id));
        }
    }
}
