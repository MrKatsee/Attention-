using Attention.Data;
using Attention.Process;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class EndingPresenter : ViewPresenter<UI_Ending>
    {
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;

        public EndingPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnExitClick);
        }

        private void OnExitClick()
        {
            _viewContainer.DeactivateView(ViewType.Ending);
        }

        private void OnEndng(EndViewEvent _event)
        {
            _viewContainer.ActivateView(ViewType.Ending);
        }
    }
}