using Attention.Data;
using System;
using UnityEngine;

namespace Attention
{
    public class CatObject : IGameObject
    {
        private Guid id;
        private Vector3 position;
        private bool direction;

        private float colliderX;
        private float colliderY;

        private float stateCooltime;

        enum BehaviorState { idle, walk }
        BehaviorState bState;
        private int stateCount;

        public CatObject(Guid id) 
        {
            this.id = id;
            bState = BehaviorState.idle;
            colliderX = 5;
            colliderY = 5;
            stateCooltime = UnityEngine.Random.Range(5.0f, 8.0f);
            stateCount = System.Enum.GetValues(typeof(BehaviorState)).Length;
        }

        public (Guid, EntityData) GetEntityData()
        {
            return (id, new EntityData { position = position, direction = direction });
        }

        public Vector3 GetPos()
        {
            return position;
        }

        public void IsCollision(IGameObject obj)
        {
            if (obj != null)
            {
                this.position = obj.GetPos();
            }
        }

        public void Update(float deltaTime)
        {
            switch (bState) 
            {
                case BehaviorState.idle:
                    //idle(deltaTime);
                    break;
                case BehaviorState.walk:
                    walk(deltaTime);
                    break;
            }

            stateCooltime -= deltaTime;
            if (stateCooltime < 0)
            {
                bState = (BehaviorState)UnityEngine.Random.Range(0, stateCount);
                stateCooltime = UnityEngine.Random.Range(5.0f, 8.0f);
            }
        }

        //private void idle(float deltaTime)
        //{

        //}

        private void walk(float deltaTime)
        {
            position += new Vector3(3, 0, 0) * (direction ? 1 : -1) * deltaTime;

            direction = UnityEngine.Random.Range(0.0f, 100.0f) > 99.9f ? !direction : direction;
        }
    }
}