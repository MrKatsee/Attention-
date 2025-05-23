using Attention.View;
using Attention.Window;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Attention.View
{
    
    public class WindowThumbnail : MonoBehaviour
    {

        [SerializeField] private Button _button;
        [SerializeField] private RawImage _thumbnail;
        [SerializeField] private Text _title;

        private Action _onClick;
        public void Init(WindowAPIData data, Action action)
        {
            _thumbnail.texture = data.Thumbnail;
            _title.text = data.Title;
            _button.onClick.AddListener(()=> { action.Invoke(); });
        }
    }
}
