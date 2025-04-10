using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinApiTest : MonoBehaviour
{
    [SerializeField]
    private WindowPrefabController _controller;
    [SerializeField]
    private WindowPrefabController _prefab;
    [SerializeField]
    private Transform _viewPort;
    [SerializeField] private Text _text;
    [SerializeField] private Timer _timer;

    WindowData currentFocusedWindow = new WindowData();

    WindowData registeredWindow = new WindowData();

    private void Start()
    {
        WindowData data = WinApiController.Instance.GetWindowData(WinApiController.Instance.window);
        Debug.Log(data.hWnd);
        _controller.setPrefabData(data);

        var list = WinApiController.Instance.GetWindowDataList();
        foreach (var item in list)
        {
            WindowPrefabController p = Instantiate(_prefab, _viewPort);
            p.setOnClick((WindowData windowData) =>
            {
                registeredWindow = windowData;
                _text.text = windowData.title;
                
            });
            p.setPrefabData(item);

        }

    }

    private void Update()
    {
        DetectFocusedWindowChange();checkRegister();
    }

    private void checkRegister()
    {
        if (registeredWindow.Equals(currentFocusedWindow))
        {
            _timer.StartStopwatch();
        }
        else
        {
            _timer.StopStopwatch();
        }
    }

    private void DetectFocusedWindowChange()
    {
        WindowData temp = WinApiController.Instance.GetFocusedWindowData();

        if (temp != null &&
    (currentFocusedWindow == null || !currentFocusedWindow.Equals(temp)))
        {
            currentFocusedWindow = temp;

            Debug.Log($"Focused window changed: {currentFocusedWindow.title}");
            Debug.Log($"Focused window changed: {currentFocusedWindow.hWnd}");

            // 여기서 thumbnail 표시나 이벤트 처리 가능
        }
    }

}
