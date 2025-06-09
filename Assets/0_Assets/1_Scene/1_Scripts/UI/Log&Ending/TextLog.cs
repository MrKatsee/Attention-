using System;
using UnityEngine;
using UnityEngine.UI;

namespace Attention
{
    public class TextLog : MonoBehaviour
    {
        [SerializeField] private Text startTimeText;
        [SerializeField] private Text maxRecordText;
        [SerializeField] private Text cosumedCoinText;
        [SerializeField] private Text remainCoinText;
        [SerializeField] private Text maximumUsingItemText;

        public void SetText(DateTime startTime, float maxRecord, float remainCoin, float numOfCoin, string maximumUsingItem)
        {
            startTimeText.text = "시작 날짜 : " + startTime.ToString("yyyy/MM/dd");
            maxRecordText.text = "최고 기록 : " + maxRecord + "시간";
            cosumedCoinText.text = "소비한 재화 : " + (int)numOfCoin + "원";
            remainCoinText.text = "남은 재화 : " + (int)remainCoin + "원";
            Debug.Log(maximumUsingItem);
            maximumUsingItemText.text = "가장 많이 쓴 아이템 : \n(" + maximumUsingItem + ")";
        }
    }
}