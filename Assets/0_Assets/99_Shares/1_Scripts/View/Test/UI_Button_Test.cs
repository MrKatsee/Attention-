using Attention.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button_test : UI_Base
{
    [SerializeField] private Button _button;

    public override ViewType Type => ViewType.Test;

    public void SetButtonListener(Action evt)
    {
        _button.onClick.AddListener(() => { evt.Invoke(); });
    }
}
