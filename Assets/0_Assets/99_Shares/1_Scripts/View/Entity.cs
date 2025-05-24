using Attention.Data;
using System;
using UnityEngine;

namespace Attention.View
{
    public abstract class Entity : MonoBehaviour
    {
        public abstract EntityType type { get; }

        public virtual void Init() { }

        public virtual void UpdateEntity(EntityData data)
        {
            this.transform.position = data.position;
        }
    }
}