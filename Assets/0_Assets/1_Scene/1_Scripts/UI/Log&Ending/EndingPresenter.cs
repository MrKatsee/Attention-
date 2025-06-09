using Attention.Data;
using Attention.Main.EventModule;
using Attention.Process;
using System;
using UnityEngine;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class EndingPresenter : ViewPresenter<UI_Ending>
    {
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private CatDataContainer _catDataContainer;

        Sprite sprite;
        string title, desc;
        float score;
        Guid id;

        public EndingPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnLogViewClick);
        }

        public override void OnActivateView()
        {
            View.OnEnding(sprite, title, score, desc);
        }

        private void OnExitClick()
        {
            _viewContainer.DeactivateView(ViewType.Ending);
        }

        private void OnLogViewClick()
        {
            _eventQueue.EnqueueViewEvent(new LogViewUpdateEvent(this.id));
            _viewContainer.DeactivateView(ViewType.Ending); 
        }

        private void OnEnding(EndViewEvent _event)
        {
            this.id = _event.id;
            string ending = _catDataContainer.GetCatData(this.id).Ending;
            ResourceContainer resourceContainer = GameObject.FindAnyObjectByType<ResourceContainer>();
            sprite = resourceContainer.GetSprite(ending);
            title = resourceContainer.GetEndingTitle(ending);
            desc = resourceContainer.GetEndingDesc(ending);
            score = _catDataContainer.GetCatData(this.id).score;

            _viewContainer.ActivateView(ViewType.Ending);
        }
    }
}