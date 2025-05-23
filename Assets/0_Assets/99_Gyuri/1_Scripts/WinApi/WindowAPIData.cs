
using System;
using UnityEngine;

namespace Attention.Window
{
    public class WindowAPIData
    {
        public IntPtr HWnd { get; private set; }
        private string _title;
        private Texture2D _thumbnail;
        private string _exePath;

        public WindowAPIData(IntPtr hWnd, string title = null, Texture2D thumbnail = null, string exePath = null)
        {
            HWnd = hWnd;
            _title = title == null ? "" : title;
            _thumbnail = thumbnail;
            _exePath = exePath;
        }

        public void setThumbnail(Texture2D thumbnail)
        {
            _thumbnail = thumbnail;
        }

        public bool IsHWnd(WindowAPIData data)
        {
            return HWnd == data.HWnd;
        }

        public bool IsExePath(string exePath)
        {
            return _exePath.Equals(exePath);
        }
    }

}
