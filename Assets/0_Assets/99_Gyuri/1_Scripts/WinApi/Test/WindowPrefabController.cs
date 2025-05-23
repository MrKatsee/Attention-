//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Xml.Serialization;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class WindowPrefabController : MonoBehaviour, IPointerClickHandler
//{
//    [SerializeField]
//    private RawImage _thumbnail;
//    [SerializeField]
//    private Text _title;

//    private WindowData _data;

//    Action<WindowData> onClick;

//    public void setOnClick(Action<WindowData> action)
//    {
//        onClick = action;
//    }
//    public void OnPointerClick(PointerEventData eventData)
//    {
//        onClick(_data);
//    }

//    public void setPrefabData(WindowData data)
//    {
//        _data = data;
//        _thumbnail.texture = data.thumbnail;
//        _title.text = data.title;
//    }
//}

