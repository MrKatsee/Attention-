using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class WindowCaptureTest : MonoBehaviour
{
    // ���� Ȱ�� ������ �ڵ��� �������� WinAPI
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    // �������� ��ġ�� ũ�⸦ �������� WinAPI
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

    // �������� DC(Device Context)�� �������� WinAPI
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(IntPtr hWnd);

    // DC ����
    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    // ��Ʈ�� ��� ����(Blt) �Լ� - ȭ�� ����
    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(
        IntPtr hdcDest, int xDest, int yDest, int width, int height,
        IntPtr hdcSrc, int xSrc, int ySrc, uint rop);

    // ȣȯ DC ����
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    // ȣȯ ��Ʈ�� ����
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int height);

    // DC�� ������Ʈ ����
    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

    // ������Ʈ ����
    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    // DC ����
    [DllImport("gdi32.dll")]
    private static extern bool DeleteDC(IntPtr hdc);

    // ��Ʈ�� �ȼ� ������ ����
    [DllImport("gdi32.dll")]
    private static extern bool GetDIBits(
        IntPtr hdc,
        IntPtr hbmp,
        uint uStartScan,
        uint cScanLines,
        byte[] lpvBits,
        ref BITMAPINFO lpbi,
        uint uUsage
        );

    // RECT ����ü: ������ ��ġ ������ ����
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    // BITMAPINFOHEADER ����ü: ��Ʈ���� �⺻ ����
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

    // BITMAPINFO ����ü: ��Ʈ�� ���� + ���� ����
    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public uint[] bmiColors;
    }

    // ĸó�� �̹����� ǥ���� Unity UI RawImage
    public RawImage displayImage;

    void Start()
    {
        // ������ �� ȭ�� ĸó �� ǥ��
        CaptureAndShow();
    }

    // ������ ȭ���� ĸó�ϰ� UI�� ǥ��
    public void CaptureAndShow()
    {
        IntPtr hWnd = GetActiveWindow(); // ���� Ȱ��ȭ�� ������ �ڵ� ��������
        if (hWnd == IntPtr.Zero)
        {
            Debug.LogError("No active window found.");
            return;
        }

        Texture2D tex = WinApiController.Instance.CaptureWindow(hWnd); // ������ ȭ�� ĸó
        if (tex != null)
        {
            Debug.Log("Captured window and displaying texture.");
            displayImage.texture = tex; // RawImage�� ǥ��
        }
        else
        {
            Debug.LogError("Window capture failed.");
        }
    }

    // ������ ȭ���� ĸó�Ͽ� Texture2D�� ��ȯ
    private Texture2D CaptureWindow(IntPtr hWnd)
    {
        if (!GetWindowRect(hWnd, out RECT rect)) // ������ ��ġ ���� ���
        {
            Debug.LogError("GetWindowRect failed.");
            return null;
        }

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        IntPtr hdcSrc = GetWindowDC(hWnd); // ������ DC ��������
        IntPtr hdcMem = CreateCompatibleDC(hdcSrc); // �޸� DC ����
        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height); // ȣȯ ��Ʈ�� ����
        IntPtr hOld = SelectObject(hdcMem, hBitmap); // ��Ʈ���� DC�� ����

        const uint SRCCOPY = 0x00CC0020;
        bool success = BitBlt(hdcMem, 0, 0, width, height, hdcSrc, 0, 0, SRCCOPY); // ȭ�� ����

        if (!success)
        {
            Debug.LogError("BitBlt failed.");
            return null;
        }

        // ��Ʈ�� ���� ����
        BITMAPINFO bmi = new BITMAPINFO();
        bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
        bmi.bmiHeader.biWidth = width;
        bmi.bmiHeader.biHeight = height; // bottom-up ���
        bmi.bmiHeader.biPlanes = 1;
        bmi.bmiHeader.biBitCount = 32;
        bmi.bmiHeader.biCompression = 0; // BI_RGB

        int pixelBytes = width * height * 4;
        byte[] pixels = new byte[pixelBytes]; // �ȼ� �����͸� ���� �迭

        bool gotBits = GetDIBits(hdcMem, hBitmap, 0, (uint)height, pixels, ref bmi, 0); // �ȼ� ����
        if (!gotBits)
        {
            Debug.LogError("GetDIBits failed.");
            return null;
        }

        // Unity���� ����� Texture2D ����
        Texture2D tex = new Texture2D(width, height, TextureFormat.BGRA32, false);
        tex.LoadRawTextureData(pixels); // �ȼ� ������ ����
        tex.Apply();

        // ���ҽ� ����
        SelectObject(hdcMem, hOld);
        DeleteObject(hBitmap);
        DeleteDC(hdcMem);
        ReleaseDC(hWnd, hdcSrc);

        return tex;
    }
}
