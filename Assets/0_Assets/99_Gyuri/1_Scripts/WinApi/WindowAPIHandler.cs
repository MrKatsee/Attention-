using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using Util;

namespace Attention.Window
{
    [DIPublisher]
    public class WindowAPIHandler
    {
        [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")] private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")] private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
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
        [DllImport("gdi32.dll")] private static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")] private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")] private static extern bool GetDIBits(IntPtr hdc, IntPtr hbmp, uint start, uint lines, byte[] buffer, ref BITMAPINFO bmi, uint usage);
        [DllImport("user32.dll")] static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
        [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)] private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);
        [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);
        [DllImport("kernel32.dll", SetLastError = true)][return: MarshalAs(UnmanagedType.Bool)] private static extern bool CloseHandle(IntPtr hObject);
        [DllImport("user32.dll", SetLastError = true)] private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


        private const int GWL_EXSTYLE = -20;
        private const int GWL_STYLE = -16;
        private const uint WS_EX_LAYERED = 0x00080000;
        private const uint WS_EX_TOOLWINDOW = 0x00000080;
        private const uint WS_CHILD = 0x40000000;
        private const uint LWA_COLORKEY = 0x00000001;
        private const uint COLORKEY = 0xFFFFFF;
        private const uint PROCESS_QUERY_INFORMATION = 0x0400;
        private const uint PROCESS_VM_READ = 0x0010;
        private const uint SWP_NOMOVE = 0x0001;
        private const uint SWP_NOSIZE = 0x0002;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_SHOWWINDOW = 0x0040;

        private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private static readonly HashSet<string> ExcludedClassNames = new()
        {
            "ApplicationFrameWindow", // UWP 앱
            "Windows.UI.Core.CoreWindow",
            "Progman",
            "Button",
            "Shell_TrayWnd",
            "DV2ControlHost"
        };

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

        
        public WindowAPIHandler()
        {
            DI.Register(this);
        }

        public List<WindowAPIData> GetWindowDataList()
        {
            List<WindowAPIData> result = new();
            EnumWindows((hWnd, lParam) =>
            {
                if (IsRealAppWindow(hWnd))
                {
                    WindowAPIData data = GetWindowData(hWnd);
                    data.Thumbnail=CaptureWindow(hWnd);
                    result.Add(data);
                }

                return true;
            }, IntPtr.Zero);
            return result;
        }

        public WindowAPIData GetWindowData(IntPtr hWnd)
        {
            string title = GetWindowTitle(hWnd);
            string className = GetClassName(hWnd);
            string exePath = GetWindowExecutablePath(hWnd);
            if (string.IsNullOrWhiteSpace(title))
                title = "Untitled";
            Debug.Log($"{title} [{className}], Path: {exePath}");
            return new WindowAPIData(hWnd, $"{title} - {className} ", null, exePath);
        }
    
        public void SetWindowTransparent(WindowAPIData window)
        {
            int exStyle = GetWindowLong(window.HWnd, GWL_EXSTYLE);
            SetWindowLong(window.HWnd, GWL_EXSTYLE, exStyle | (int)WS_EX_LAYERED);
            SetLayeredWindowAttributes(window.HWnd, COLORKEY, 0, LWA_COLORKEY);
        }

        public void SetWindowBottom(WindowAPIData window)
        {
            SetWindowPos(window.HWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
        }

        public WindowAPIData GetFocusedWindowData()
        {

            IntPtr hWnd = GetForegroundWindow();

            if (hWnd == IntPtr.Zero || !IsRealAppWindow(hWnd))
                return null;

            return GetWindowData(hWnd);
        }
        
        private bool IsToolWindow(IntPtr hWnd) => (GetWindowLong(hWnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW) != 0;
        private string GetWindowExecutablePath(IntPtr hWnd)
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

        private string GetClassName(IntPtr hWnd)
        {
            StringBuilder s = new(256);
            GetClassName(hWnd, s, s.Capacity);
            return s.ToString();
        }

        private string GetWindowTitle(IntPtr hWnd)
        {
            int len = GetWindowTextLength(hWnd);
            StringBuilder s = new(len + 1);
            GetWindowText(hWnd, s, s.Capacity);
            return s.ToString();
        }

        private bool IsRealAppWindow(IntPtr hWnd)
        {
            //기본 예외 처리
            if (!IsWindowVisible(hWnd)) return false;
            if (GetParent(hWnd) != IntPtr.Zero) return false;
            if (GetWindowTextLength(hWnd) == 0) return false;
            long style = GetWindowLongPtr(hWnd, GWL_STYLE).ToInt64();
            if ((style & WS_CHILD) != 0) return false;

            // UWP 앱 검사
            string className = GetClassName(hWnd);
            if (ExcludedClassNames.Contains(className)) return false;

            //그 외 Util 앱 검사
            if (IsToolWindow(hWnd)) return false;

            return true;
        }

        //윈도우 캡처 : 캡쳐 시 창이 하나 더 생기기 때문에 자주 호출하면 깜빡임 현상 있음.
        private Texture2D CaptureWindow(IntPtr hWnd)
        {
            if (!GetWindowRect(hWnd, out RECT rect)) return null;

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            IntPtr hdcSrc = GetWindowDC(hWnd);
            IntPtr hdcMem = CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
            IntPtr old = SelectObject(hdcMem, hBitmap);

            // BitBlt -> PrintWindow로 변경
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
    }
}

