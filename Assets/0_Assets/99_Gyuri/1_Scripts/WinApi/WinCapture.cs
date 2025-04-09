using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class WindowCapture : MonoBehaviour
{

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(
        IntPtr hdcDest, int xDest, int yDest, int width, int height,
        IntPtr hdcSrc, int xSrc, int ySrc, uint rop);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int height);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern bool GetDIBits(
        IntPtr hdc,
        IntPtr hbmp,
        uint uStartScan,
        uint cScanLines,
        byte[] lpvBits,
        ref BITMAPINFO lpbi,
        uint uUsage);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFOHEADER
    {
        public uint biSize;
        public int biWidth;
        public int biHeight;
        public ushort biPlanes;
        public ushort biBitCount;
        public uint biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public uint[] bmiColors;
    }

    public RawImage displayImage;

    void Start()
    {
        CaptureAndShow();
    }

    public void CaptureAndShow()
    {
        IntPtr hWnd = GetActiveWindow();
        if (hWnd == IntPtr.Zero)
        {
            Debug.LogError("No active window found.");
            return;
        }

        Texture2D tex = CaptureWindow(hWnd);
        if (tex != null)
        {
            Debug.Log("Captured window and displaying texture.");
            displayImage.texture = tex;
        }
        else
        {
            Debug.LogError("Window capture failed.");
        }
    }

    private Texture2D CaptureWindow(IntPtr hWnd)
    {
        if (!GetWindowRect(hWnd, out RECT rect))
        {
            Debug.LogError("GetWindowRect failed.");
            return null;
        }

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        IntPtr hdcSrc = GetWindowDC(hWnd);
        IntPtr hdcMem = CreateCompatibleDC(hdcSrc);
        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
        IntPtr hOld = SelectObject(hdcMem, hBitmap);

        const uint SRCCOPY = 0x00CC0020;
        bool success = BitBlt(hdcMem, 0, 0, width, height, hdcSrc, 0, 0, SRCCOPY);

        if (!success)
        {
            Debug.LogError("BitBlt failed.");
            return null;
        }

        BITMAPINFO bmi = new BITMAPINFO();
        bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
        bmi.bmiHeader.biWidth = width;
        bmi.bmiHeader.biHeight = height; // bottom-down
        bmi.bmiHeader.biPlanes = 1;
        bmi.bmiHeader.biBitCount = 32;
        bmi.bmiHeader.biCompression = 0; // BI_RGB

        int pixelBytes = width * height * 4;
        byte[] pixels = new byte[pixelBytes];

        bool gotBits = GetDIBits(hdcMem, hBitmap, 0, (uint)height, pixels, ref bmi, 0);
        if (!gotBits)
        {
            Debug.LogError("GetDIBits failed.");
            return null;
        }

        Texture2D tex = new Texture2D(width, height, TextureFormat.BGRA32, false);
        tex.LoadRawTextureData(pixels);
        tex.Apply();

        // Clean up
        SelectObject(hdcMem, hOld);
        DeleteObject(hBitmap);
        DeleteDC(hdcMem);
        ReleaseDC(hWnd, hdcSrc);

        return tex;
    }
}
