using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class WinApiController : MonoBehaviour
{
    // === WinAPI ��� ���� ===
    const int GWL_EXSTYLE = -20;
    const int GWL_STYLE = -16;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TOOLWINDOW = 0x00000080;
    const uint WS_CHILD = 0x40000000;
    const uint WS_POPUP = 0x80000000;
    const uint LWA_COLORKEY = 0x00000001;
    const uint SWP_SHOWWINDOW = 0x0040;

    private static WinApiController instance;

    public static WinApiController Instance => instance;

    public IntPtr window { get; private set; } = IntPtr.Zero;

    private static readonly HashSet<string> ExcludedClassNames = new()
{
    "ApplicationFrameWindow", // UWP ��
    "Windows.UI.Core.CoreWindow",
    "Progman",
    "Button",
    "Shell_TrayWnd",
    "DV2ControlHost"
};

    // === Unity �̺�Ʈ ===
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

    // === WinAPI �Լ� ���� ===
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")] public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")] private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")] private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll")] private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    [DllImport("user32.dll")] private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int GetWindowTextLength(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll")] private static extern IntPtr GetParent(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true)] private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
    [DllImport("user32.dll")] private static extern IntPtr GetWindowDC(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")] private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")] private static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll")] private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
    [DllImport("gdi32.dll")] private static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);
    [DllImport("gdi32.dll")] private static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int w, int h, IntPtr hdcSrc, int xSrc, int ySrc, uint rop);
    [DllImport("gdi32.dll")] private static extern bool DeleteDC(IntPtr hdc);
    [DllImport("gdi32.dll")] private static extern bool DeleteObject(IntPtr hObject);
    [DllImport("gdi32.dll")] private static extern bool GetDIBits(IntPtr hdc, IntPtr hbmp, uint start, uint lines, byte[] buffer, ref BITMAPINFO bmi, uint usage);

    [DllImport("user32.dll")] static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseHandle(IntPtr hObject);

    private const uint PROCESS_QUERY_INFORMATION = 0x0400;
    private const uint PROCESS_VM_READ = 0x0010;

    public string GetWindowExecutablePath(IntPtr hWnd)
    {
        GetWindowThreadProcessId(hWnd, out uint processId);

        IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, processId);
        if (hProcess == IntPtr.Zero)
            return null;

        StringBuilder buffer = new StringBuilder(1024);
        GetModuleFileNameEx(hProcess, IntPtr.Zero, buffer, buffer.Capacity);

        CloseHandle(hProcess);
        return buffer.ToString();
    }



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

    // === â ���͸� �� ���� ���� ===
    private bool IsToolWindow(IntPtr hWnd) => (GetWindowLong(hWnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW) != 0;

    private string GetClassNameString(IntPtr hWnd)
    {
        StringBuilder sb = new(256);
        GetClassName(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    private string GetWindowTitle(IntPtr hWnd)
    {
        int len = GetWindowTextLength(hWnd);
        StringBuilder sb = new(len + 1);
        GetWindowText(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    private bool IsRealAppWindow(IntPtr hWnd)
    {
        if (!IsWindowVisible(hWnd)) return false;
        if (GetParent(hWnd) != IntPtr.Zero) return false;

        // â Ÿ��Ʋ�� ������ ����
        if (GetWindowTextLength(hWnd) == 0) return false;

        // ������ Ŭ���� �̸�
        string className = GetClassNameString(hWnd);
        if (ExcludedClassNames.Contains(className)) return false;

        // ��Ÿ�� �˻�
        long style = GetWindowLongPtr(hWnd, GWL_STYLE).ToInt64();
        if ((style & WS_CHILD) != 0) return false;

        // �������� ����
        if (IsToolWindow(hWnd)) return false;


        return true;
    }

    public List<WindowData> GetWindowDataList()
    {
        List<WindowData> result = new();
        EnumWindows((hWnd, lParam) =>
        {
            if (IsRealAppWindow(hWnd))
            {
                WindowData data = GetWindowData(hWnd);
                Texture2D thumbnail = CaptureWindow(hWnd);
                data.thumbnail = thumbnail;   
                result.Add(data);
            }
                
            return true;
        }, IntPtr.Zero);
        return result;
    }

    public WindowData GetWindowData(IntPtr hWnd)
    {
        string title = GetWindowTitle(hWnd);
        string className = GetClassNameString(hWnd);
        //Texture2D thumbnail = CaptureWindow(hWnd);
        string exePath = GetWindowExecutablePath(hWnd);
        if (string.IsNullOrWhiteSpace(title))
            title = "Untitled";
        //Debug.Log($"{title} [{className}], Path: {exePath}");
        return new WindowData(hWnd, $"{title} [{className}]", null, exePath);
    }


    // === ĸó �Լ� ===


    public Texture2D CaptureWindow(IntPtr hWnd)
    {
        if (!GetWindowRect(hWnd, out RECT rect)) return null;

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        IntPtr hdcSrc = GetWindowDC(hWnd);
        IntPtr hdcMem = CreateCompatibleDC(hdcSrc);
        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
        IntPtr old = SelectObject(hdcMem, hBitmap);

        // BitBlt -> PrintWindow�� ����
        bool success = PrintWindow(hWnd, hdcMem, 0);

        BITMAPINFO bmi = new()
        {
            bmiHeader = new BITMAPINFOHEADER
            {
                biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER)),
                biWidth = width,
                biHeight = height, // top-down
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

        return success ? tex : null;
    }



    // === â�� �����ϰ� ���� ===
    public void MakeWindowTransparent()
    {
        int exStyle = GetWindowLong(window, GWL_EXSTYLE);
        SetWindowLong(window, GWL_EXSTYLE, exStyle | (int)WS_EX_LAYERED);
        SetLayeredWindowAttributes(window, 0, 0, LWA_COLORKEY);
    }
    public WindowData GetFocusedWindowData()
    {
        IntPtr hWnd = GetForegroundWindow();

        if (hWnd == IntPtr.Zero || !IsRealAppWindow(hWnd))
            return null;

        return GetWindowData(hWnd);
    }

    public bool IsRegisteredWindowStillOpen(IntPtr hWnd)
    {
        var currentWindows = WinApiController.Instance.GetWindowDataList();
        foreach (var win in currentWindows)
        {
            if (win.hWnd == hWnd)
            {
                return true;
            }
        }
        return false;
    }




}
