using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.Window
{

    public class UI_WIndowSelect : UI_Base
    {
        
        public override ViewType Type => ViewType.WindowSelect;

        private Transform _scrollViewport;

        private List<WindowThumbnail> thumbnails;

        private void Init(List<WindowAPIData> windows)
        {
            foreach (var window in windows)
            {
                
            }
        }


    }

}
