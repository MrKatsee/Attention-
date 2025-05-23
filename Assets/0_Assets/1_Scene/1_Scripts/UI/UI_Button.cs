using Attention.View;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Attention
{
    public abstract class UI_Button : UI_Base
    {
        [SerializeField] private Button _button;

        public void SetButtonListener(Action evt)
        {
            _button.onClick.AddListener(() => { evt.Invoke(); });
        }
    }

}