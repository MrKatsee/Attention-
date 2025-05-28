using System;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{
    
    public class WindowThumbnail : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RawImage _thumbnail;
        [SerializeField] private Text _title;

        public void SetThumbnail(Texture2D thumbnail)
        {
            _thumbnail.texture = thumbnail;
        }
        public void SetTitle(string title)
        {
            _title.text = title;
        }
        public void SetListener(Action<int> action, int index)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => { action(index); });
        }
    }

}
