using System.Collections.Generic;

namespace Attention.Data
{
    public struct CatData
    {
        public string name;

        public float Happiness;    // �ູ��
        public float Bond;         // ���밨
        public float Fullness;     // ������
        public float Cleanliness;  // û�ᵵ

        public List<State> stateLogs;

        public CatData(string name)
        {
            this.name = name;

            this.Happiness = 50.0f;
            this.Bond = 0.0f;
            this.Fullness = 50.0f;
            this.Cleanliness = 50.0f;

            this.stateLogs = new List<State>();
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
        public float Happiness { get; private set; } 
        public float Bond { get; private set; } 
        public float Fullness { get; private set; } 
        public float Cleanliness { get; private set; }

        public CatChangeData(float Happiness, float Bond, float Fullness, float Cleanliness)
        {
            this.Happiness = Happiness;
            this.Bond = Bond;
            this.Fullness = Fullness;
            this.Cleanliness = Cleanliness;
        }

        public CatChangeData(CatData data)
        {
            this.Happiness = data.Happiness;
            this.Bond = data.Bond;
            this.Fullness = data.Fullness;
            this.Cleanliness = data.Cleanliness;
        }
    }
}