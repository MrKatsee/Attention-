using Attention.Data;
using Attention.Main.EventModule;
using System;
using UnityEngine;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class CatBehaviorHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject] private EntityDataContainer _entityDataContainer;
        [Inject] private GameObjectContainer _objectContainer;
        [Inject] private BehaviorContainer _behaviorContainer;

        int catBehaviorCount = 3;
        float behaviorTime = 5;
        float catSpeed = 5;
        float gravityScale = 2.0f;

        public CatBehaviorHandler() 
        {
            DI.Register(this);
        }

        public void Update(DeltaTimeEvent _event)
        {
            if(_behaviorContainer == null)
            {
                Debug.Log("??");
                return;
            }

            foreach (Guid id in _behaviorContainer.GetGuids())
            {
                EntityData data = _entityDataContainer.GetEntityData(id);
                if(data == null)
                {
                    continue;
                }

                IBehaviorData behavior = _behaviorContainer.GetBehavior(id);

                if (behavior.Do(ref data, _event.DeltaTime))
                {
                    _behaviorContainer.Register(id, GetRandomCatBehavior());
                }
                _entityDataContainer.UpdatetEntityData(id, data);
                _eventQueue.EnqueueViewEvent(new EntityUpdateEvent(id));
            }

            //foreach (var pair in _objectContainer.GetAllData())
            //{
            //    UnityEngine.Debug.Log("[Before] Pos : " + pair.Item2.position + ", Direction : " + pair.Item2.direction);
            //}

/*            _objectContainer.Update(_event.DeltaTime);

            foreach (var pair in _objectContainer.GetAllData())
            {
                //UnityEngine.Debug.Log("[After] Pos : " + pair.Item2.position + ", Direction : " + pair.Item2.direction);
                _entityDataContainer.UpdatetEntityData(pair.Item1, pair.Item2);
                _eventQueue.EnqueueViewEvent(new EntityUpdateEvent(pair.Item1));
            }*/
        }

        public void MouseInput(SelectClickEvent _event)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(_event.ScreenPosition);
            Debug.Log("Click!! : " + mousePos);

            foreach (Guid id in _behaviorContainer.GetGuids())
            {
                EntityData entity = _entityDataContainer.GetEntityData(id);
                if (!entity.isCollerable)
                {
                    Debug.Log(id + " is not collerable!");
                    continue;
                }

                if (entity.position.x + 0.75f > mousePos.x 
                    && entity.position.x - 0.75f < mousePos.x
                    && entity.position.y + 0.9f > mousePos.y
                    && entity.position.y - 0.9f < mousePos.y)
                {
                    Debug.Log(id + " is Clicked!!");
                    MouseTraceBehavior behavior = new MouseTraceBehavior();
                    behavior.SetMousePos(mousePos);
                    _behaviorContainer.Register(id, behavior);
                }
            }
        }

        public void DragInput(ShiftSelectClickEvent _event)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(_event.ScreenPosition);
            Debug.Log("Drag!! : " + mousePos);

            foreach (Guid id in _behaviorContainer.GetGuids())
            {
                EntityData entity = _entityDataContainer.GetEntityData(id);
                if (!entity.isCollerable)
                {
                    Debug.Log(id + " is not collerable!");
                    continue;
                }

                IBehaviorData behavior = _behaviorContainer.GetBehavior(id);
                if (behavior is MouseTraceBehavior mouseTrace) { mouseTrace.SetMousePos(mousePos); }
            }
        }

        public void MouseUp(ExitSelectClickEvent _event)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(_event.ScreenPosition);
            Debug.Log("Up!! : " + mousePos);

            foreach (Guid id in _behaviorContainer.GetGuids())
            {
                EntityData entity = _entityDataContainer.GetEntityData(id);
                if (!entity.isCollerable)
                {
                    Debug.Log(id + " is not collerable!");
                    continue;
                }

                IBehaviorData behavior = _behaviorContainer.GetBehavior(id);
                if (behavior is MouseTraceBehavior mouseTrace) 
                { 
                    _behaviorContainer.Register(id, new FallenBehavior(UnityEngine.Random.Range(-11.0f, -9.0f), gravityScale));
                }
                
            }
        }

        public IBehaviorData GetRandomCatBehavior()
        {
            int random = UnityEngine.Random.Range(0, catBehaviorCount);
            switch(random)
            {
                default:
                case 0:
                    return new IdleBehavior(UnityEngine.Random.Range(2.0f, behaviorTime));
                case 1:
                    return new MoveBehavior(new Vector3(UnityEngine.Random.Range(-28.0f, 28.0f), UnityEngine.Random.Range(-11.0f, -9.0f), 0), catSpeed);
                case 2:
                    return new LayBehavior(UnityEngine.Random.Range(1.0f, behaviorTime));
            }
        }
    }
}