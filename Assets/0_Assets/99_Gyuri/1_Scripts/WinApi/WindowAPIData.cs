
using System;
using UnityEngine;

namespace Attention.Window
{
    public class WindowAPIData
    {
        private IntPtr _hwnd;
        private string _title;
        private Texture2D _thumbnail;
        private string _exePath;

        public WindowAPIData()
        {
            _hwnd = IntPtr.Zero;
        }
        public WindowAPIData(IntPtr hWnd, string title = null, Texture2D thumbnail = null, string exePath = null)
        {
            _hwnd = hWnd;
            _title = title == null ? "" : title;
            _thumbnail = thumbnail;
            _exePath = exePath;
        }

        public IntPtr HWnd{
            get { return _hwnd; }
        }
        public string Title
        {
            get { return _title; }
        }
        public Texture2D Thumbnail
        {
            get { return _thumbnail; }
            set { _thumbnail = value; }
        }

        public string exePath
        {
            get { return exePath; }
        }

        public bool IsHWnd(WindowAPIData data)
        {
            return _hwnd == data._hwnd;
        }

        public bool IsExePath(string exePath)
        {
            return _exePath.Equals(exePath);
        }
    }

}
