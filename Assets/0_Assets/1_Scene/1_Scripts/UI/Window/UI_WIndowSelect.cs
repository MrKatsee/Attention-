using Attention.View;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.Window
{
    public class UI_WIndowSelect : UI_Base
    {
        public override ViewType Type => ViewType.WindowSelect;

        private const int MAX_INDEX = 20;

        [SerializeField] private List<WindowThumbnail> _thumbnails;
        [SerializeField] private Transform _scrollContent;
        [SerializeField] private Transform _thumbnailContainer;
        [SerializeField] private Button _quitButton;

        public void UpdateThumbnails(List<WindowAPIData> windows, Action<int> action)
        {
            for(int i=0;i<windows.Count;i++)
            {
                if (i >= MAX_INDEX) break;
                WindowThumbnail thumbnail = _thumbnails[i];
                thumbnail.transform.SetParent(_scrollContent);
                thumbnail.gameObject.SetActive(true);
                thumbnail.SetThumbnail(windows[i].Thumbnail);
                thumbnail.SetTitle(windows[i].Title);
                thumbnail.SetListener(action, i);
                
            }
        }
        public void ResetThumbnails()
        {
            foreach (var thumbnail in _thumbnails)
            {
                thumbnail.transform.SetParent(_thumbnailContainer);
                thumbnail.gameObject.SetActive(false);
            }
        }

        public void Init(Action quitAction)
        {
            _quitButton.onClick.AddListener(() => quitAction());
        }
        


    }

}
