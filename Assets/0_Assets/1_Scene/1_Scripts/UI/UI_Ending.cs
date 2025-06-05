using Attention.View;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Attention
{
    public class UI_Ending : UI_Base
    {
        public override ViewType Type => ViewType.Ending;

        [SerializeField] private Button _exitBtn;
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;

        public void Init(Action onClickExit)
        {
            _exitBtn.onClick.AddListener(() => onClickExit());
        }

        public void OnEnding(Sprite sprite, string text)
        {
            _text.text = text;
            _image.sprite = sprite;
        }
    }
}