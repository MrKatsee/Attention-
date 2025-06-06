using Attention.Data;
using Attention.Main.EventModule;
using Attention.Process;
using Attention.Window;
using UnityEngine;
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

        [Inject(typeof(PlayerDataContainer))] private IPlayerDataContainer _playerDataContainer;

        public MenuPanelPresenter()
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            //View.Init(OnClickSettingButton, OnClickShopButton, OnClickWindowButton);
            View.Init(OnClickSettingButton, OnClickEndingButton, OnClickShopButton, OnClickWindowButton);
        }

        public override void OnActivateView()
        {
            UpdateMoney();
        }

        private void OnClickSettingButton()
        {
            _viewContainer.ActivateView(ViewType.SettingPanel);
        }

        private void OnClickShopButton()
        {
            _viewContainer.ActivateView(ViewType.Shop);
            _viewContainer.ActivateView(ViewType.State);
            _eventQueue.EnqueueViewEvent(new CurrentCatStateViewEvent());
        }

        private void OnClickEndingButton()
        {
            //_viewContainer.ActivateView(ViewType.Ending);
            _eventQueue.EnqueueLogicEvent(new EndEvent());
        }

        private void OnClickWindowButton()
        {
            _windowAPIHandler.SetWindowBottom(_windowDataContainer.AttentionWindowData);
        }

        private void UpdateMoney()
        {
            View.SetMoney(Mathf.RoundToInt(_playerDataContainer.GetMoney()));
        }

        public void UpdateMoney(UpdateMoneyEvent data)
        {
            UpdateMoney();
        }


    }
}

