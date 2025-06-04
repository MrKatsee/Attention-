using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class MenuPanelPresenter : ViewPresenter<UI_MenuPanel>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject(typeof(WindowAPIHandler))] private WindowAPIHandler _windowAPIHandler;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;

        public MenuPanelPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnClickSettingButton, OnClickShopButton, OnClickWindowButton);
        }

        private void OnClickSettingButton()
        {
            _viewContainer.ActivateView(ViewType.SettingPanel);
        }

        private void OnClickShopButton()
        {
            _viewContainer.ActivateView(ViewType.Shop);
        }

        private void OnClickWindowButton()
        {
            _windowAPIHandler.SetWindowBottom(_windowDataContainer.AttentionWindowData);
        }
    }
}

