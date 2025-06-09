using System;
using UnityEngine;
using UnityEngine.UI;

public class DataBlock : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dateText;

    public void Init(Guid id, string name, DateTime startTime, Action<Guid> callDataAction, Sprite Ending)
    {
        button.onClick.AddListener(() => { callDataAction(id); });
        image.sprite = Ending;
        nameText.text = name;
        dateText.text = startTime.ToString("yy/MM/dd");
    }

    public void Init(Sprite baseImage)
    {
        image.sprite = baseImage; 
        nameText.text = "(데이터 없음)";
        dateText.text = "";
    }
}