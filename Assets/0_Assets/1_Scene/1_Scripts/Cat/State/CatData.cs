using System.Collections.Generic;

namespace Attention.Data
{
    public struct CatData
    {
        public string name;

        public float satiety;
        public float stress;
        public float affection;

        public List<State> states;
    }

    public struct State
    {
        public float satiety;
        public float stress;
        public float affection;
    }

    public struct CatChangeData
    {
        public CatChangeData(float satiety, float stress, float affection)
        {
            this.satiety = satiety;
            this.stress = stress;
            this.affection = affection;
        }

        public float satiety;
        public float stress;
        public float affection;
    }
}