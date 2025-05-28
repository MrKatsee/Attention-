using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.View
{
    public class ObjPrefabContainer : MonoBehaviour
    {
        [SerializeField] private List<Obj_Base> _objs;

        private Dictionary<ViewType, Obj_Base> _objDict;

        public void Init()
        {
            _objDict = new Dictionary<ViewType, Obj_Base>();

            foreach (Obj_Base obj in _objs)
            {
                _objDict.Add(obj.Type, obj);
            }
        }

        public Obj_Base GetObj(ViewType type)
        {
            if(_objDict.TryGetValue(type, out Obj_Base obj))
            {
                return obj;
            }
            else
            {
                Debug.LogError($"Object of type {type} not found.");
                return null;
            }
        }
    }
}