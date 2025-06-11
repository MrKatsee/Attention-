using Attention.Data;

namespace Attention
{
    public class IdleBehavior : IBehaviorData
    {
        float time;
        public IdleBehavior(float time)
        {
            this.time = time;
        }

        public bool Do(ref EntityData data, float deltaTime)
        {
            data.animator = (int)CatBehaviorType.idle;
            time -= deltaTime;
            return time < 0;
        }
    }
}