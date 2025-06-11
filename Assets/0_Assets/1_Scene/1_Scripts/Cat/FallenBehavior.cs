using Attention.Data;
using UnityEngine;

namespace Attention
{
    public class FallenBehavior : IBehaviorData
    {
        float targetY;
        float gravityScale;
        float speed;

        public FallenBehavior(float targetY, float gravityScale)
        {
            this.targetY = targetY;
            this.gravityScale = gravityScale;
            this.speed = 0;
        }

        public bool Do(ref EntityData data, float deltaTime)
        {
            data.position = new Vector3(data.position.x, data.position.y - speed, 0);
            speed += gravityScale * deltaTime;
            if(data.position.y < targetY)
            {
                data.position.y = targetY;
                return true;
            }
            return false;
        }
    }
}