using Attention.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attention
{
    public class UI_Shop : UI_Base
    {
        public override ViewType Type => ViewType.Shop;

        [SerializeField] private Button _exitBtn;

        public void Init(Action onExitClick)
        {
            _exitBtn.onClick.AddListener(() => onExitClick());
        }
    }
}