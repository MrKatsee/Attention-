using Attention.Main.EventModule;
using Util;

namespace Attention
{

}

namespace Attention.View
{
    [DISubscriber]
    public class MenuPanelPresenter : ViewPresenter<UI_MenuPanel>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;

        public MenuPanelPresenter()
        {
            DI.Register(this);
        }
        protected override void OnInitializeView()
        {
            View.Init(() =>
            {
                _viewContainer.ActivateView(ViewType.SettingPanel);
            });
        }
    }

}

