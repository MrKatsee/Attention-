using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class ActivateTransparentTest : MonoBehaviour
{
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow(); // 현재 활성 창 핸들 가져오기

    private static Dictionary<string, IntPtr> registeredWindows = new Dictionary<string, IntPtr>();

    /// <summary>
    /// 실행 중인 창 목록 가져오기
    /// </summary>
    public static void RegisterWindows()
    {
        registeredWindows.Clear();

        EnumWindows((hWnd, lParam) =>
        {
            if (IsWindowVisible(hWnd))
            {
                StringBuilder title = new StringBuilder(256);
                GetWindowText(hWnd, title, title.Capacity);
                if (title.Length > 0)
                {
                    registeredWindows[title.ToString()] = hWnd;
                    Debug.Log("Registered Window: " + title);
                }
            }
            return true;
        }, IntPtr.Zero);
    }
}
