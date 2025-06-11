using Attention.Data;
using UnityEngine;

namespace Attention
{
    public class MoveBehavior : IBehaviorData
    {
        private Vector3 goalPos;
        private float speed;

        public MoveBehavior(Vector3 goalPos, float speed)
        {
            this.goalPos = goalPos;
            this.goalPos.z = 0;
            this.speed = speed;
        }

        public bool Do(ref EntityData data, float deltaTime)
        {
            data.position = Vector3.Normalize(goalPos - data.position) * speed * deltaTime + data.position;
            data.animator = (int)CatBehaviorType.walk;
            return Vector3.Distance(data.position, goalPos) < 1f;
        }
    }
}