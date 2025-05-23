using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention
{
    [DISubscriber]
    public class ShopPresenter : ViewPresenter<UI_Shop>
    {
        [Inject(typeof(ViewContainer))] private IViewLoader _viewContainer;

        public ShopPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnExitClick);
        }

        private void OnExitClick()
        {
            _viewContainer.DeactivateView(ViewType.Shop);
        }
    }
}
