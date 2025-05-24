using Attention.Main.EventModule;
using Attention.View;
using Util;

namespace Attention
{
    [DISubscriber]
    public class CreateCatPresenter : ViewPresenter<UI_CreateCat>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;

        public CreateCatPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnCreateCat);
        }

        private void OnCreateCat(string name)
        {
            _eventQueue.EnqueueLogicEvent(new CreateCatEvent(name));
            _viewContainer.DeactivateView(ViewType.CreateCat);
        }
    }
}