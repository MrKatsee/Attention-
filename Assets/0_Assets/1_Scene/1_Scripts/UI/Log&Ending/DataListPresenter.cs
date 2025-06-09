using System;
using Attention.Data;
using System.Collections.Generic;
using Util;
using Attention.Main.EventModule;
using Attention.Process;
using UnityEngine;

namespace Attention.View
{
    [DISubscriber]
    public class DataListPresenter : ViewPresenter<UI_DataList>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject] private CatDataContainer _catDataContainer;

        public DataListPresenter()
        {
            DI.Register(this); 
        }

        protected override void OnInitializeView()
        {
            View.Init(NewStartButton, GetSprite);
        }

        public override void OnActivateView()
        {
            List<(Guid, CatData)> list = _catDataContainer.GetAllDatas();
            View.Set(list, LoadLogView);
        }

        //public void OnUdpate(DataVIewUpdateEvent _event)
        //{
        //    List<(Guid, CatData)> list = _catDataContainer.GetAllDatas();
        //    View.Set(list, LoadLogView);
        //}

        public Sprite GetSprite(string name)
        {
            return GameObject.FindAnyObjectByType<ResourceContainer>().GetSprite(name);
        }

        public void LoadLogView(Guid id)
        {
            _eventQueue.EnqueueViewEvent(new EndViewEvent(id));
            _viewContainer.DeactivateView(ViewType.DataList);
        }

        public void NewStartButton()
        {
            _viewContainer.DeactivateView(ViewType.DataList);
            _eventQueue.EnqueueLogicEvent(new StartEvent());
        }
    }
}