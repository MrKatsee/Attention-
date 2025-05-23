using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Attention.Window
{
    public class WindowThumbnail : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RawImage _thumbnail;
        [SerializeField] private Text _title;

        private Action _onClick;

        public void Init(Action action)
        {
            _button.onClick.AddListener(()=> { action.Invoke(); });
        }
    }
}
