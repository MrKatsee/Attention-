using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Window
{
    public class UI_WIndowSelect : UI_Base
    {
        
        public override ViewType Type => ViewType.WindowSelect;

        [SerializeField] private List<WindowThumbnail> _thumbnails;
        private Transform _scrollContent;
        private Transform _thumbnailContainer;

        private void Init(List<WindowAPIData> windows)
        {
            for(int i=0;i<windows.Count;i++)
            {
                WindowThumbnail thumbnail = _thumbnails[i];
                thumbnail.transform.SetParent(_scrollContent);
                thumbnail.gameObject.SetActive(true);
                
            }
            foreach (var thumbnail in _thumbnails)
            {
                //WindowThumbnail w = Instantiate(_windowThumbnailPrefab, _scrollViewport);
                //w.Init(window, () =>
                //{
                //    //윈도우 이벤트
                //});
            }
        }

        public void OnDeactivate()
        {
            foreach (var thumbnail in _thumbnails)
            {
                thumbnail.transform.SetParent(_thumbnailContainer);
                thumbnail.gameObject.SetActive(false);
            }
        }
        


    }

}
