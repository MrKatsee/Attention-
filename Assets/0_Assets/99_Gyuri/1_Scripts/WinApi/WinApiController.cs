using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class WinApiController : MonoBehaviour
{
    private static WinApiController instance;
    public static WinApiController Instance => instance;

    public IntPtr window { get; private set; } = IntPtr.Zero;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            window = GetActiveWindow();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // === WinAPI 선언 ===
    [DllImport("user32.dll")] private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll")] private static extern int GetWindowTextLength(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
    [DllImport("user32.dll")] private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")] private static extern IntPtr GetWindowDC(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")] private static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll")] private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
    [DllImport("gdi32.dll")] private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
    [DllImport("gdi32.dll")] private static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int width, int height, IntPtr hdcSrc, int xSrc, int ySrc, uint rop);
    [DllImport("gdi32.dll")] private static extern bool DeleteDC(IntPtr hdc);
    [DllImport("gdi32.dll")] private static extern bool DeleteObject(IntPtr hObject);
    [DllImport("gdi32.dll")] private static extern bool GetDIBits(IntPtr hdc, IntPtr hBitmap, uint uStartScan, uint cScanLines, byte[] lpvBits, ref BITMAPINFO lpbmi, uint uUsage);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT { public int Left, Top, Right, Bottom; }

    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFOHEADER
    {
        public uint biSize;
        public int biWidth, biHeight;
        public ushort biPlanes, biBitCount;
        public uint biCompression, biSizeImage;
        public int biXPelsPerMeter, biYPelsPerMeter;
        public uint biClrUsed, biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] public uint[] bmiColors;
    }

    // === 창 정보 수집 ===
    public List<WindowData> GetWindowDataList()
    {
        List<WindowData> result = new();
        EnumWindows((hWnd, lParam) =>
        {
            string title = GetWindowTitle(hWnd);
            string className = GetClassNameString(hWnd);
            Texture2D thumbnail = CaptureWindow(hWnd);

            if (string.IsNullOrWhiteSpace(title)) title = "Untitled";

            result.Add(new WindowData(hWnd, $"{title} [{className}]", thumbnail));
            return true;
        }, IntPtr.Zero);
        return result;
    }

    public WindowData GetWindowData(IntPtr hWnd)
    {
        string title = GetWindowTitle(hWnd);
        string className = GetClassNameString(hWnd);
        Texture2D thumbnail = CaptureWindow(hWnd);

        if (string.IsNullOrWhiteSpace(title)) title = "Untitled";

        return new WindowData(hWnd, $"{title} [{className}]", thumbnail);
    }

    private string GetWindowTitle(IntPtr hWnd)
    {
        int len = GetWindowTextLength(hWnd);
        StringBuilder sb = new(len + 1);
        GetWindowText(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    private string GetClassNameString(IntPtr hWnd)
    {
        StringBuilder sb = new(256);
        GetClassName(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    // === 창 캡처 ===
    public Texture2D CaptureWindow(IntPtr hWnd)
    {
        if (!GetWindowRect(hWnd, out RECT rect)) return null;

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        IntPtr hdcSrc = GetWindowDC(hWnd);
        IntPtr hdcMem = CreateCompatibleDC(hdcSrc);
        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
        IntPtr old = SelectObject(hdcMem, hBitmap);

        BitBlt(hdcMem, 0, 0, width, height, hdcSrc, 0, 0, 0x00CC0020);

        BITMAPINFO bmi = new()
        {
            bmiHeader = new BITMAPINFOHEADER
            {
                biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER)),
                biWidth = width,
                biHeight = -height, // top-down
                biPlanes = 1,
                biBitCount = 32,
                biCompression = 0
            },
            bmiColors = new uint[256]
        };

        byte[] pixels = new byte[width * height * 4];
        GetDIBits(hdcMem, hBitmap, 0, (uint)height, pixels, ref bmi, 0);

        Texture2D tex = new(width, height, TextureFormat.BGRA32, false);
        tex.LoadRawTextureData(pixels);
        tex.Apply();

        SelectObject(hdcMem, old);
        DeleteObject(hBitmap);
        DeleteDC(hdcMem);
        ReleaseDC(hWnd, hdcSrc);

        return tex;
    }
}

