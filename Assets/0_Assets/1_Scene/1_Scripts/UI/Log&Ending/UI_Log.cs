using Attention.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{ 
    public class UI_Log : UI_Base
    {
        public override ViewType Type => ViewType.Log;

        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        //[SerializeField] private Button exitButton;

        [SerializeField] private Text nameText;

        [SerializeField] private StatusLog _statusSlider;
        [SerializeField] private TextLog _textLog;
        [SerializeField] private TimeLog _timeGraph;

        private List<GameObject> logLIst;

        private int _current = 0;

        public void Init(Action exitClick)
        {
            logLIst = new List<GameObject>();
            logLIst.Add(_statusSlider.gameObject);
            logLIst.Add(_textLog.gameObject);
            //logLIst.Add(_timeGraph.gameObject);
            //exitButton.onClick.AddListener(() => { exitClick(); });
            prevButton.onClick.AddListener(() => { Previous(); });
            nextButton.onClick.AddListener(() => { Next(exitClick); });
        }

        public void Resetting(CatData data)
        {
            _current = 0;
            nameText.text = data.name;

            _statusSlider.SetSlider(data.Happiness, data.Bond, data.Fullness, data.Cleanliness);
            float maxRecord = 0;
            Dictionary<string, int> itemLog = new Dictionary<string, int>();
            List<float> record = new List<float>();
            foreach (Log log in data.logs)
            {
                if(log.timeRecord > maxRecord)
                {
                    maxRecord = log.timeRecord;
                }
                record.Add(log.timeRecord);

                foreach (string item in log.useItem)
                {
                    if(itemLog.ContainsKey(item))
                    {
                        itemLog[item]++;
                    }
                    else
                    {
                        itemLog[item] = 1;
                    }
                }
            }

            foreach (string item in data.useItem)
            {
                if (itemLog.ContainsKey(item))
                {
                    itemLog[item]++;
                }
                else
                {
                    itemLog[item] = 1;
                }
            }

            string maxUseItem = "¾øÀ½";
            int maxUseCount = 0;
            foreach (string item in itemLog.Keys)
            {
                Debug.Log(item + ": " + itemLog[item]);
                if (itemLog[item] > maxUseCount)
                {
                    maxUseItem = item;
                    maxUseCount = itemLog[item];
                }
            }

            _textLog.SetText(data.startTime, maxRecord, data.remainCoin, data.usedCoin, maxUseItem);
            _timeGraph.Set((maxRecord + 3 > 24 ? 24 : (int)maxRecord + 3), record);
            ViewLog();
        }

        private void Previous()
        {
            if(_current < 1) { return;  }
            _current--;
            ViewLog();
        }

        private void Next(Action exitEvent)
        {
            if(_current == logLIst.Count - 1) { exitEvent(); }
            _current++;
            ViewLog();
        }

        private void ViewLog()
        {
            for (int i = 0; i < logLIst.Count; i++)
            {
                bool isTarget = i == _current;
                if(isTarget)
                {

                }
                logLIst[i].SetActive(isTarget);
            }
        }
    }
}