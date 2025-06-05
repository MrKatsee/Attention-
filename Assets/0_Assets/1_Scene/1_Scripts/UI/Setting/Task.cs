using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] private Text _task;
    [SerializeField] private Button _button;

    public void SetText(string task)
    {
        _task.text = task;
    }

    public void SetListener(Action<int> action, int index)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => { action(index); });
    }
}
