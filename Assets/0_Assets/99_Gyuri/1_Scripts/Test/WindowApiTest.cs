using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class WindowApiTest
{
    const uint WS_EX_LAYERED = 0x00080000;
    const uint LWA_COLORKEY = 0x00000001;
    const int GWL_EXSTYLE = -20;
    const int HWND_TOPMOST = -1;
    const int HWND_NOTOPMOST = -2;
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_SHOWWINDOW = 0x0040;

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public List<string> windowTitles = new List<string>();
    public IntPtr selectedWindow = IntPtr.Zero;
    public bool alwaysOnTop = false;

    // 창 투명화
    public void MakeWindowTransparent(IntPtr hWnd)
    {
        int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | (int)WS_EX_LAYERED);
        SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);
    }

    // 항상 위 토글
    public void ToggleAlwaysOnTop(IntPtr hWnd)
    {
        alwaysOnTop = !alwaysOnTop;
        SetWindowPos(hWnd,
            alwaysOnTop ? HWND_TOPMOST : HWND_NOTOPMOST,
            0, 0, 0, 0,
            SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
    }

    // 창 목록 가져오기
    public void GetWindowList()
    {
        windowTitles.Clear();
        EnumWindows((hWnd, lParam) =>
        {
            StringBuilder buffer = new StringBuilder(256);
            GetWindowText(hWnd, buffer, buffer.Capacity);
            string title = buffer.ToString();
            if (!string.IsNullOrEmpty(title))
            {
                windowTitles.Add(title);
            }
            return true;
        }, IntPtr.Zero);
    }

    // 특정 창 선택
    public void SelectWindowByTitle(string title)
    {
        selectedWindow = FindWindow(null, title);
        Debug.Log($"Selected window handle: {selectedWindow}");
    }
}
