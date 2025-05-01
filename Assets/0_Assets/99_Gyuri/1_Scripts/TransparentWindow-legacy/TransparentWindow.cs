using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{
    /// â�� Client Area�� Ȯ��

    /// Window�� Client ���� : ���� ������ (�����ڰ� ���� �׸� �� �ִ� ����)
    /// non-client ����: Title, Border, �ּ�ȭ/�ִ�ȭ/�ݱ� ��ư
    /// margin�� -1 �� ������ ��� ȭ�� ��ü ������ client������ �ȴ�.

    [DllImport("dwmapi.dll")]

    private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);

    /// â�� ��ġ (��ǥ, ȭ�� �� Z-����) ����


    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    /// Attention!! ����â ��������

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    /// â�� �Ӽ��� �ο��ϴ� �Լ�

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    /// Layered �Ӽ��� ���� â�� ���� �Ӽ��� �����ϴ� �Լ�


    [DllImport("user32.dll")]
    private static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);


    [StructLayout(LayoutKind.Sequential)]
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    const int GWL_EXSTYLE = -20; // ���� WindowLong�� Ȯ�� ��Ÿ���� �����ϴ� �ε���


    const uint WS_EX_LAYERED = 0x00080000; //â�� ������ ������ �� �ִ� �÷���
    const uint LWA_COLORKEY = 0x00000001; //WS_EX_LAYERED�� ������ â�� ���� ���� �Ӽ��� ������ �� ���-crKey�� �ش��ϴ� ������ �����ϰ� �ٲ�

    static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-1); // ȭ�� Z-���� ���� ��ܿ� ��ġ�ϵ��� ��.

    private void Start()
    {
#if !UNITY_EDITOR
        SetTransparentPage();
#endif

    }


    private void SetTransparentPage()
    {
        IntPtr hWnd = GetActiveWindow();
        MARGINS margins = new MARGINS { cxLeftWidth = -1, cxRightWidth = -1, cyTopHeight = -1, cyBottomHeight = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED); // â�� �Ӽ��� �ο��ϴ� �Լ�  - GWL_EXSTYLE: ���� �Ӽ�, WS_EX_LAYERED: ���� �����ϴ� �÷��� �ο�
        SetWindowPos(hWnd, (IntPtr)HWND_NOTOPMOST, 0, 0, 0, 0, 0); // �׻� ���� ��ܿ� �����ϵ��� �����ϴ� �Լ�
        SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);//  WS_EX_LAYERED�� ����� â�� �Ӽ��� �����ϴ� �Լ� 
        // ���� �Լ� ��� : alpha = 0 �� ������ ���� ���� ����������.
        Application.runInBackground = true;
    }
}
