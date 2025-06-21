using System;
using System.Collections.Generic;

namespace Attention.Data
{
    [Serializable]
    public struct CatData
    {
        public string name;
        public DateTime startTime;

        public string Ending;
        public float usedCoin;
        public float remainCoin;
        public float score;
        public float recordTime;

        public List<string> useItem;

        public float Happiness;    // �ູ��
        public float Bond;         // ���밨
        public float Fullness;     // ������
        public float Cleanliness;  // û�ᵵ

        public List<Log> logs;

        public CatData(string name)
        {
            this.name = name;

            this.startTime = DateTime.Now;

            this.Ending = "";
            this.usedCoin = 0;
            this.recordTime = 0;
            this.remainCoin = 0;
            this.score = 0;

            this.Happiness = 10.0f;
            this.Bond = 0.0f;
            this.Fullness = 10.0f;
            this.Cleanliness = 10.0f;

            this.useItem = new List<string>();
            this.logs = new List<Log>();
        }
    }

    public struct Log
    {
        public float timeRecord;
        public State listState;
        public List<string> useItem;

        public Log(float timeRecord, float happiness, float bond, float fullness, float cleanliness)
        {
            this.timeRecord = timeRecord;
            this.listState = new State(happiness, bond, fullness, cleanliness);
            useItem = new List<string>();
        }

        public Log(float timeRecord, float happiness, float bond, float fullness, float cleanliness, List<string> usedItem)
        {
            this.timeRecord = timeRecord;
            this.listState = new State(happiness, bond, fullness, cleanliness);
            this.useItem = usedItem;
        }
    }

    public struct State
    {
        public float Happiness { get; private set; } 
        public float Bond { get; private set; }    
        public float Fullness { get; private set; } 
        public float Cleanliness { get; private set; }

        public State(float Happiness, float Bond, float Fullness, float Cleanliness)
        {
            this.Happiness = Happiness;
            this.Bond = Bond;
            this.Fullness = Fullness;
            this.Cleanliness = Cleanliness;
        }
    }

    public struct CatChangeData
    {
        public float RecordTime { get; private set; }
        public float Happiness { get; private set; } 
        public float Bond { get; private set; } 
        public float Fullness { get; private set; } 
        public float Cleanliness { get; private set; }

        public CatChangeData(float Happiness, float Bond, float Fullness, float Cleanliness)
        {
            this.RecordTime = 0;
            this.Happiness = Happiness;
            this.Bond = Bond;
            this.Fullness = Fullness;
            this.Cleanliness = Cleanliness;
        }

        public CatChangeData(float RecordTime, float Happiness, float Bond, float Fullness, float Cleanliness)
        {
            this.RecordTime = RecordTime;
            this.Happiness = Happiness;
            this.Bond = Bond;
            this.Fullness = Fullness;
            this.Cleanliness = Cleanliness;
        }
    }
}