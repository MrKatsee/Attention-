using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{
    public class UI_MenuPanel : UI_Base
    {
        public override ViewType Type => ViewType.MenuPanel;

        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _endingRoomButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _windowButton;

        [SerializeField] private Text _txtMoney;
        [SerializeField] private Text _txtSharedMoney;

        public void Init(Action onClickSetting, Action onClickShop, Action windowAction)
        {
            _settingButton.onClick.AddListener(() => { onClickSetting(); });
            _shopButton.onClick.AddListener(() => { onClickShop(); });
            _windowButton.onClick.AddListener(() => { windowAction(); });

            Canvas canvas = GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = 1000;
        }

        public void Init(Action onClickSetting, Action endingAtion, Action onClickShop, Action windowAction)
        {
            _settingButton.onClick.AddListener(() => { onClickSetting(); });
            _endingRoomButton.onClick.AddListener(() => { endingAtion(); });
            _shopButton.onClick.AddListener(() => { onClickShop(); });
            _windowButton.onClick.AddListener(() => { windowAction(); });

            Canvas canvas = GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = 1000;
        }

        public void SetMoney(int money)
        {
            _txtMoney.text = money.ToString() + "원";
        }

        public void SetMoney(int money, int sharedMoney)
        {
            _txtMoney.text = money.ToString() + "원";
            _txtSharedMoney.text = sharedMoney.ToString() + "원";
        }
    }
}

