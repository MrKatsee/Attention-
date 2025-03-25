using System;
using System.Runtime.InteropServices; //Win API
using UnityEngine;

public class TransparentTest : MonoBehaviour
{
    
    /// 창의 Client Area을 확장
    

    /// Window의 Client 영역 : 실제 컨텐츠 (개발자가 직접 그릴 수 있는 영역)
    /// non-client 영역: Title, Border, 최소화/최대화/닫기 버튼
    /// margin을 -1 로 설정할 경우 화면 전체 영역이 client영역이 된다.

    [DllImport("dwmapi.dll")]
    
    private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);
    
    /// 창의 위치 (좌표, 화면 상 Z-순서) 설정
    

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

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

    const int GWL_EXSTYLE = -20; // 현재 WindowLong의 확장 스타일을 설정하는 인덱스
 

    const uint WS_EX_LAYERED = 0x00080000; //창의 투명도를 설정할 수 있는 플래그
    const uint ES_EX_TRANSPARENT = 0x00000020; //창을 클릭할 수 없도록 하는 플래그
    const uint LWA_COLORKEY = 0x00000001; //WS_EX_LAYERED가 설정된 창에 대해 세부 속성을 설정할 때 사용-crKey에 해당하는 색상이 투명하게 바뀜

    static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-1);

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
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED); // 창의 속성을 부여하는 함수 
        SetWindowPos(hWnd, (IntPtr)HWND_NOTOPMOST, 0, 0, 0, 0, 0); // 항상 가장 상단에 존재하도록 설정하는 함수
        SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);//  WS_EX_LAYERED이 적용된 창의 속성을 설정하는 함수
        // 현재 함수 결과 : alpha = 0 인 검정색 색상에 대해 투명해진다.
        Application.runInBackground = true;
    }

    
}
