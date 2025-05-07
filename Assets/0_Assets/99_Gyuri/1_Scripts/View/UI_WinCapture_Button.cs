using Attention.View;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{
    public class UI_WinCapture_Button : UI_Base
    {
        public override ViewType Type => ViewType.WinCapture_Button;
        [SerializeField] private Button _button;

        public void SetButtonListener(Action evt)
        {
            _button.onClick.AddListener(()=>evt.Invoke());
        }

    }


}
