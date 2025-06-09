using Attention.View;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Attention
{
    public class UI_CreateCat : UI_Base
    {
        public override ViewType Type => ViewType.CreateCat;

        [SerializeField] private Button _btn;
        [SerializeField] private InputField _inputCatData;

        public void Init(Action<string> onClick)
        {
            _btn.onClick.AddListener(() => onClick(_inputCatData.text));
        }

        public void Resetting()
        {
            _inputCatData.text = "";
        }
    }
}