using Attention.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Window
{
    public class UI_WIndowSelect : UI_Base
    {
        public override ViewType Type => ViewType.WindowSelect;

        private const int MAX_INDEX = 20;

        [SerializeField] private List<WindowThumbnail> _thumbnails;
        private Transform _scrollContent;
        private Transform _thumbnailContainer;

        public void Init(List<WindowAPIData> windows, Action<int> action)
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
        


    }

}
