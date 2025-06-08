using Attention.Data;
using Attention.Main.EventModule;
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
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;

        public EndingPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnExitClick, OnLogViewClick);
        }

        private void OnExitClick()
        {
            _viewContainer.DeactivateView(ViewType.Ending);
        }

        private void OnLogViewClick()
        {
            _eventQueue.EnqueueViewEvent(new LogViewUpdateEvent());
            _viewContainer.ActivateView(ViewType.Log);
            _viewContainer.DeactivateView(ViewType.Ending);
        }

        private void OnEndng(EndViewEvent _event)
        {
            _viewContainer.ActivateView(ViewType.Ending);
        }
    }
}