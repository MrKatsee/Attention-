using UnityEngine;

namespace Attention.View
{
    public abstract class UI_Base : MonoBehaviour, IView
    {
        public abstract ViewType Type { get; }
    }
}