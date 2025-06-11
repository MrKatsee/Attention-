using Attention.Data;
using System;
using System.Collections.Generic;
using Util;

namespace Attention
{
    public interface IBehaviorData
    {
        public bool Do(ref EntityData data, float deltaTime);
    }

    [DIPublisher]
    public class BehaviorContainer
    {
        Dictionary<Guid, IBehaviorData> _behaviorDict;

        public BehaviorContainer()
        {
            _behaviorDict = new Dictionary<Guid, IBehaviorData>();
            DI.Register(this);
        }

        public void Register(Guid id, IBehaviorData behavior)
        {
            _behaviorDict[id] = behavior;
        }

        public List<Guid> GetGuids()
        {
            List<Guid> list = new List<Guid>();
            foreach (Guid id in _behaviorDict.Keys)
            {
                list.Add(id);
            }
            return list;
        }

        public IBehaviorData GetBehavior(Guid id)
        {
            if (_behaviorDict.ContainsKey(id)) { return _behaviorDict[id]; }
            return null;
        }

        public void Clear()
        {
            _behaviorDict.Clear();
            UnityEngine.Debug.Log("Clear : " + _behaviorDict.Count);
        }
    }
}