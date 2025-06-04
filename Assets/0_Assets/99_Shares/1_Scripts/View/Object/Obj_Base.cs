using UnityEngine;

namespace Attention.View
{
    public abstract class Obj_Base : MonoBehaviour, IView
    {
        public abstract ViewType Type { get; }
    }
}