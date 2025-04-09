using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinApiTest : MonoBehaviour
{
    [SerializeField]
    private WindowPrefabController _controller;
    [SerializeField]
    private WindowPrefabController _prefab;
    [SerializeField]
    private Transform _viewPort;

    private void Start()
    {
        WindowData data = WinApiController.Instance.GetWindowData(WinApiController.Instance.window);
        Debug.Log(data.hWnd);
        _controller.setPrefabData(data);

        var list = WinApiController.Instance.GetWindowDataList();
        foreach (var item in list)
        {
            WindowPrefabController p = Instantiate(_prefab, _viewPort);
            p.setPrefabData(item);

        }

    }
}
