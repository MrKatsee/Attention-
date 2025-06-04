using Attention.View;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TaskTimer : UI_Base
{
    public override ViewType Type => ViewType.TaskTimer;

    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;

    public void SetTimer(string time)
    {
        _timer.text = time;
    }

    public void Init(Action startAction, Action stopAction)
    {
        _startButton.onClick.AddListener(() => { 
            startAction();
        });
        _stopButton.onClick.AddListener(() => {
            stopAction();
        });

    }

    public void SetButton(bool isWorking)
    {
        _stopButton.gameObject.SetActive(isWorking);
    }



}
