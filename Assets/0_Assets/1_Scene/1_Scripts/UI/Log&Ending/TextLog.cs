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
            startTimeText.text = "���� ��¥ : " + startTime.ToString("yyyy/MM/dd");
            maxRecordText.text = "�ְ� ��� : " + maxRecord + "�ð�";
            cosumedCoinText.text = "�Һ��� ��ȭ : " + (int)numOfCoin + "��";
            remainCoinText.text = "���� ��ȭ : " + (int)remainCoin + "��";
            Debug.Log(maximumUsingItem);
            maximumUsingItemText.text = "���� ���� �� ������ : \n(" + maximumUsingItem + ")";
        }
    }
}