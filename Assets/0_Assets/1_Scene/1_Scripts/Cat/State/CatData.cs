using System.Collections.Generic;

namespace Attention.Data
{
    public struct CatData
    {
        public string name;

        public float Happiness;    // 행복함
        public float Bond;         // 유대감
        public float Fullness;     // 포만감
        public float Cleanliness;  // 청결도

        public List<State> stateLogs;

        public CatData(string name)
        {
            this.name = name;

            this.Happiness = 0.0f;
            this.Bond = 0.0f;
            this.Fullness = 0.0f;
            this.Cleanliness = 0.0f;

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