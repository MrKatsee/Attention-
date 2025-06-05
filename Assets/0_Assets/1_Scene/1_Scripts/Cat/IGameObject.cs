using Attention.Data;
using System;
using UnityEngine;

public interface IGameObject
{ 
    public (Guid, EntityData) GetEntityData();
    public Vector3 GetPos();
    public void Update(float deltaTime);
    public void IsCollision(IGameObject obj);
}