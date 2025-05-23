using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{
    public class UI_Cat : UI_Base
    {
        public override ViewType Type => ViewType.Cat;

        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] private Text _text;

        public void SetData(string str)
        {
            _text.text = str;
        }
    }
}