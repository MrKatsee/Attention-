using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class WindowCaptureTest : MonoBehaviour
{
    // 현재 활성 윈도우 핸들을 가져오는 WinAPI
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    // 윈도우의 위치와 크기를 가져오는 WinAPI
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

    // 윈도우의 DC(Device Context)를 가져오는 WinAPI
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(IntPtr hWnd);

    // DC 해제
    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    // 비트맵 블록 전송(Blt) 함수 - 화면 복사
    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(
        IntPtr hdcDest, int xDest, int yDest, int width, int height,
        IntPtr hdcSrc, int xSrc, int ySrc, uint rop);

    // 호환 DC 생성
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    // 호환 비트맵 생성
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int height);

    // DC에 오브젝트 선택
    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

    // 오브젝트 제거
    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    // DC 제거
    [DllImport("gdi32.dll")]
    private static extern bool DeleteDC(IntPtr hdc);

    // 비트맵 픽셀 데이터 추출
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

    // RECT 구조체: 윈도우 위치 정보를 담음
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    // BITMAPINFOHEADER 구조체: 비트맵의 기본 정보
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

    // BITMAPINFO 구조체: 비트맵 정보 + 색상 정보
    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public uint[] bmiColors;
    }

    // 캡처한 이미지를 표시할 Unity UI RawImage
    public RawImage displayImage;

    void Start()
    {
        // 시작할 때 화면 캡처 및 표시
        CaptureAndShow();
    }

    // 윈도우 화면을 캡처하고 UI에 표시
    public void CaptureAndShow()
    {
        IntPtr hWnd = GetActiveWindow(); // 현재 활성화된 윈도우 핸들 가져오기
        if (hWnd == IntPtr.Zero)
        {
            Debug.LogError("No active window found.");
            return;
        }

        Texture2D tex = WinApiController.Instance.CaptureWindow(hWnd); // 윈도우 화면 캡처
        if (tex != null)
        {
            Debug.Log("Captured window and displaying texture.");
            displayImage.texture = tex; // RawImage에 표시
        }
        else
        {
            Debug.LogError("Window capture failed.");
        }
    }

    // 윈도우 화면을 캡처하여 Texture2D로 반환
    private Texture2D CaptureWindow(IntPtr hWnd)
    {
        if (!GetWindowRect(hWnd, out RECT rect)) // 윈도우 위치 정보 얻기
        {
            Debug.LogError("GetWindowRect failed.");
            return null;
        }

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        IntPtr hdcSrc = GetWindowDC(hWnd); // 윈도우 DC 가져오기
        IntPtr hdcMem = CreateCompatibleDC(hdcSrc); // 메모리 DC 생성
        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height); // 호환 비트맵 생성
        IntPtr hOld = SelectObject(hdcMem, hBitmap); // 비트맵을 DC에 선택

        const uint SRCCOPY = 0x00CC0020;
        bool success = BitBlt(hdcMem, 0, 0, width, height, hdcSrc, 0, 0, SRCCOPY); // 화면 복사

        if (!success)
        {
            Debug.LogError("BitBlt failed.");
            return null;
        }

        // 비트맵 정보 설정
        BITMAPINFO bmi = new BITMAPINFO();
        bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
        bmi.bmiHeader.biWidth = width;
        bmi.bmiHeader.biHeight = height; // bottom-up 방식
        bmi.bmiHeader.biPlanes = 1;
        bmi.bmiHeader.biBitCount = 32;
        bmi.bmiHeader.biCompression = 0; // BI_RGB

        int pixelBytes = width * height * 4;
        byte[] pixels = new byte[pixelBytes]; // 픽셀 데이터를 담을 배열

        bool gotBits = GetDIBits(hdcMem, hBitmap, 0, (uint)height, pixels, ref bmi, 0); // 픽셀 추출
        if (!gotBits)
        {
            Debug.LogError("GetDIBits failed.");
            return null;
        }

        // Unity에서 사용할 Texture2D 생성
        Texture2D tex = new Texture2D(width, height, TextureFormat.BGRA32, false);
        tex.LoadRawTextureData(pixels); // 픽셀 데이터 적용
        tex.Apply();

        // 리소스 정리
        SelectObject(hdcMem, hOld);
        DeleteObject(hBitmap);
        DeleteDC(hdcMem);
        ReleaseDC(hWnd, hdcSrc);

        return tex;
    }
}
