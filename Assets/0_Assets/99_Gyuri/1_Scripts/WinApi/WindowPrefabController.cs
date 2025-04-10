using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowPrefabController : MonoBehaviour
{
    [SerializeField]
    private RawImage _thumbnail;
    [SerializeField]
    private Text _title;

    public void setPrefabData(WindowData data)
    {
        _thumbnail.texture = data.thumbnail;
        _title.text = data.title;
    }
}
