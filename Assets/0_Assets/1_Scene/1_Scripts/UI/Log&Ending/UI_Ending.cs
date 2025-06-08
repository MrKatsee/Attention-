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
        [SerializeField] private Button _logViewBtn;
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;

        public void Init(Action onClickExit, Action onClickLogView)
        {
            _exitBtn.onClick.AddListener(() => onClickExit());
            _logViewBtn.onClick.AddListener(() => onClickLogView());
        }

        public void OnEnding(Sprite sprite, string text)
        {
            _text.text = text;
            _image.sprite = sprite;
        }
    }
}