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

        [SerializeField] private Button _logViewBtn;
        [SerializeField] private Text _title;
        [SerializeField] private Text _score;
        [SerializeField] private Text _desc;
        [SerializeField] private Image _image;

        public void Init(Action onClickLogView)
        {
            _logViewBtn.onClick.AddListener(() => onClickLogView());
        }

        public void OnEnding(Sprite sprite, string title, float score, string desc)
        {
            _title.text = title;
            _image.sprite = sprite;
            _score.text = (int)score + "Á¡!";
            _desc.text = desc;
        }
    }
}