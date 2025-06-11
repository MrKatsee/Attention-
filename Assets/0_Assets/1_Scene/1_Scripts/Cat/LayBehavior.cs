using Attention.Data;

namespace Attention
{
    public class LayBehavior : IBehaviorData
    {
        float time;

        public LayBehavior(float time)
        {
            this.time = time;
        }

        public bool Do(ref EntityData data, float deltaTime)
        {
            data.animator = (int)CatBehaviorType.Lay;
            time -= deltaTime;
            return time < 0;
        }
    }
}