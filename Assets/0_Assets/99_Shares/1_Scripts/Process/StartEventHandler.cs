using Attention.Data;
using Attention.View;
using System;
using Util;

namespace Attention.Process
{
    public class StartEvent : ILogicEvent { }

    [DISubscriber]
    public class StartEventHandler : ILogicEventHandler
    {
        [Inject(typeof(ViewLoader))] private IViewLoader _viewLoader;
        [Inject] private CatDataContainer _catContainer;

        public StartEventHandler()
        {
            DI.Register(this);
        }

        public void OnGameStart(StartEvent _event)
        {
            //기존에 고양이가 있으면 지우기
            if (_catContainer.currentCatId != Guid.Empty)
            {
                //
            }

            _viewLoader.ActivateView(ViewType.CreateCat);
        }
    }
}