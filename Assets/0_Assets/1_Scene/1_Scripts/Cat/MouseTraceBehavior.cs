using Attention.Data;
using UnityEngine;

namespace Attention
{
    public class MouseTraceBehavior : IBehaviorData
    {
        Vector3 mousePos;

        public void SetMousePos(Vector3 mousePos)
        {
            this.mousePos = mousePos;
            this.mousePos.z = 0;
        }

        public bool Do(ref EntityData data, float deltaTime)
        {
            data.position = this.mousePos;
            data.animator = (int)CatBehaviorType.BeCatched;
            return false;
        }
    }
}