using Attention.Data;
using Attention.Main.EventModule;
using Attention.Process;
using Attention.View;
using System;
using System.Diagnostics;
using Util;

namespace Attention
{
    [DISubscriber]
    public class LogViewPrsenter : ViewPresenter<UI_Log>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject] private CatDataContainer _catContainer;
        Guid id;

        public LogViewPrsenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnExitClick);
        }

        //public override void OnActivateView()
        //{
        //    View.Resetting(_catContainer.GetCatData(_catContainer.currentCatId));
        //}

        public override void OnActivateView()
        {
            base.OnActivateView();
            View.Resetting(_catContainer.GetCatData(id));
        }

        public void Set(LogViewUpdateEvent data)
        {
            if(data.id == Guid.Empty)
            {
                id = _catContainer.currentCatId;
            }
            else
            {
                id = data.id;  
            }

            _viewContainer.ActivateView(ViewType.Log);
        }

        private void OnExitClick()
        {
            _viewContainer.DeactivateView(ViewType.Log);
            _viewContainer.ActivateView(ViewType.DataList);
        }
    }
}