using Attention.View;
using TMPro;
using UnityEngine;

public class UI_TaskTimer : UI_Base
{
    public override ViewType Type => ViewType.TaskTimer;

    [SerializeField] private TextMeshProUGUI _timer;

    public void SetTimer(string time)
    {
        _timer.text = time;
    }



}
