using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;


public class WinApiController : MonoBehaviour
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
    private static extern IntPtr GetActiveWindow();

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

    [DllImport("user32.dll")]
    static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern IntPtr GetParent(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);



    const uint WS_EX_TOOLWINDOW = 0x00000080;

    delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public List<WindowData> WindowList
    {
        get; private set;
    } = new List<WindowData>();
    //private List<WindowData> _windowList = new List<WindowData>();
    private List<WindowData> _selectedWindowList = new List<WindowData>();

    private IntPtr _window = IntPtr.Zero;
    


    public static WinApiController Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private static WinApiController instance = null;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _window = GetActiveWindow();
        MakeWindowTransparent();
    }

    public void MakeWindowTransparent()
    {
        int exStyle = GetWindowLong(_window, GWL_EXSTYLE);
        SetWindowLong(_window, GWL_EXSTYLE, exStyle | (int)WS_EX_LAYERED);
        SetLayeredWindowAttributes(_window, 0, 0, LWA_COLORKEY);
    }

    private bool IsToolWindow(IntPtr hWnd)
    {
        return (GetWindowLong(hWnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW) != 0;
    }
    private bool IsUserVisibleWindow(IntPtr hWnd)
    {
        return IsWindowVisible(hWnd) &&
               GetWindowTextLength(hWnd) > 0 &&
               GetParent(hWnd) == IntPtr.Zero &&
               !IsToolWindow(hWnd);
    }

    // 창 목록 가져오기
    public void GetWindowList()
    {
        WindowList.Clear();
        EnumWindows((hWnd, lParam) =>
        {
            if (IsUserVisibleWindow(hWnd))
            {
                int length = GetWindowTextLength(hWnd);
                StringBuilder sb = new StringBuilder(length + 1);
                GetWindowText(hWnd, sb, sb.Capacity);
                string title = sb.ToString();
                StringBuilder classBuffer = new StringBuilder(256);
                GetClassName(hWnd, classBuffer, classBuffer.Capacity);
                string className = classBuffer.ToString();

                if (string.IsNullOrWhiteSpace(title))
                {
                    title = "무제 창 (Untitled)";
                }

                WindowList.Add(new WindowData(hWnd,title+ className));
            }

            return true;
        }, IntPtr.Zero);
    }

    [DllImport("user32.dll")]
    static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

    [DllImport("gdi32.dll")]
    static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest,
        int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

    [DllImport("gdi32.dll")]
    static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    static extern bool DeleteDC(IntPtr hdc);

    [DllImport("user32.dll")]
    static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll")]
    static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    const int SRCCOPY = 0x00CC0020;

    public Texture2D CaptureWindowThumbnail(IntPtr hWnd, int thumbWidth = 200, int thumbHeight = 150)
    {
        // 창의 클라이언트 영역 크기 가져오기
        GetClientRect(hWnd, out RECT rect);
        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        if (width <= 0 || height <= 0)
            return null;

        // 원본 크기로 캡처
        IntPtr hdcSrc = GetDC(hWnd);
        IntPtr hdcMem = CreateCompatibleDC(hdcSrc);
        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
        IntPtr hOld = SelectObject(hdcMem, hBitmap);

        BitBlt(hdcMem, 0, 0, width, height, hdcSrc, 0, 0, SRCCOPY);

        Bitmap bmp = Image.FromHbitmap(hBitmap);

        // 리소스 해제
        SelectObject(hdcMem, hOld);
        DeleteObject(hBitmap);
        DeleteDC(hdcMem);
        ReleaseDC(hWnd, hdcSrc);

        // 썸네일 크기로 리사이즈
        Bitmap resized = new Bitmap(bmp, new System.Drawing.Size(thumbWidth, thumbHeight));

        // Bitmap → Texture2D 변환
        Texture2D tex = new Texture2D(resized.Width, resized.Height, TextureFormat.RGBA32, false);
        for (int y = 0; y < resized.Height; y++)
        {
            for (int x = 0; x < resized.Width; x++)
            {
                System.Drawing.Color color = resized.GetPixel(x, resized.Height - y - 1); // Y 뒤집기
                tex.SetPixel(x, y, new Color32(color.R, color.G, color.B, color.A));
            }
        }
        tex.Apply();

        bmp.Dispose();
        resized.Dispose();

        return tex;
    }



}
