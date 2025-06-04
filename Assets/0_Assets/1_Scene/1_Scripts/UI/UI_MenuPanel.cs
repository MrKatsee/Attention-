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

        public void Init(Action onClickSetting, Action onClickShop)
        {
            _settingButton.onClick.AddListener(() => { onClickSetting(); });
            _shopButton.onClick.AddListener(() => { onClickShop(); });
        }
    }
}

