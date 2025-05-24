using Attention.Main.EventModule;
using Attention.View;
using System;
using UnityEngine;
using Util;

public abstract class Entity : MonoBehaviour
{
    public Guid id { get; private set; }

    public abstract EntityType type {  get; }

    public virtual void Init()
    {
        this.id = Guid.NewGuid();
    }

    public abstract void UpdateEntity();
}
