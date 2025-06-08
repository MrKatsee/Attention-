using System;
using UnityEngine;
using UnityEngine.UI;

public class DataBlock : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dateText;

    public void Init(Guid id, string name, DateTime startTime, Action<Guid> callDataAction)
    {
        button.onClick.AddListener(() => { callDataAction(id); });
        nameText.text = name;
        dateText.text = startTime.ToString("yy/MM/dd");
    }

    public void Init()
    {
        nameText.text = "";
        dateText.text = "";
    }
}