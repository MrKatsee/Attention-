using System;
using UnityEngine;

namespace Attention.Window
{
    public class WindowAPIData
    {
        public IntPtr HWnd { get; private set; }
        public string Title { get; private set; }
        public Texture2D Thumbnail { get; private set; }
        public string ExePath { get; private set; }

        public WindowAPIData()
        {
            HWnd = IntPtr.Zero;
        }

        public WindowAPIData(IntPtr hWnd, string title = null, Texture2D thumbnail = null, string exePath = null)
        {
            HWnd = hWnd;
            Title = title == null ? "" : title;
            Thumbnail = thumbnail;
            ExePath = exePath;
        }

        public void SetThumbnail(Texture2D thumbnail)
        {
            Thumbnail = thumbnail;
        }
    }

}
