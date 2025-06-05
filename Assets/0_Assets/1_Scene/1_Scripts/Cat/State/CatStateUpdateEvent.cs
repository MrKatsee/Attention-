using Attention.Data;
using System;

namespace Attention
{
    public class CatStateUpdateEvent : ILogicEvent
    {
        public Guid id;
        public CatChangeData data;

        public CatStateUpdateEvent(Guid id, float Happiness, float Bond, float Fullness, float Cleanliness)
        {
            this.id = id;
            data = new CatChangeData(Happiness, Bond, Fullness, Cleanliness);
        }

        public CatStateUpdateEvent(Guid id, CatChangeData data)
        {
            this.id = id;
            this.data = data;
        }   
    }

    public class CurrentCatStateUpdateEvent : ILogicEvent
    {
        public CatChangeData data;

        public CurrentCatStateUpdateEvent(float Happiness, float Bond, float Fullness, float Cleanliness)
        {
            this.data = new CatChangeData(Happiness, Bond, Fullness, Cleanliness);
        }

        public CurrentCatStateUpdateEvent(CatChangeData data)
        {
            this.data = data;
        }
    }

    public class CatStateViewEvent : IViewEvent
    {
        public Guid id;
        public State state;

        public CatStateViewEvent(Guid id, float Happiness, float Bond, float Fullness, float Cleanliness)
        {
            this.id = id;
            this.state = new State(Happiness, Bond, Fullness, Cleanliness);
        }

        public CatStateViewEvent(Guid id, State state)
        {
            this.id = id;
            this.state = state;
        }
    }
}
