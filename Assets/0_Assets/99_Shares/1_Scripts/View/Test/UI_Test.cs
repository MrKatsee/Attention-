using TMPro;
using UnityEngine;

namespace Attention.View
{
    public class UI_Test : UI_Base
    {
        public override ViewType Type => ViewType.Test;

        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}