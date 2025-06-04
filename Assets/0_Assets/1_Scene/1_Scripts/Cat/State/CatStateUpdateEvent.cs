using System;

namespace Attention
{
    public class CatStateUpdateEvent : ILogicEvent
    {
        public Guid id;
        public float satiety;
        public float stress;
        public float affection;

        public CatStateUpdateEvent(Guid id, float satiety, float stress, float affection)
        {
            this.id = id;
            this.satiety = satiety;
            this.stress = stress;
            this.affection = affection;
        }
    }

    public class CurrentCatStateUpdateEvent : ILogicEvent
    {
        public float satiety;
        public float stress;
        public float affection;

        public CurrentCatStateUpdateEvent(float satiety, float stress, float affection)
        {
            this.satiety = satiety;
            this.stress = stress;
            this.affection = affection;
        }
    }

    public class CatStateViewEvent : IViewEvent
    {
        public Guid id;
        public float satiety;
        public float stress;
        public float affection;

        public CatStateViewEvent(Guid id, float satiety, float stress, float affection)
        {
            this.id = id;
            this.satiety = satiety;
            this.stress = stress;
            this.affection = affection;
        }
    }
}
