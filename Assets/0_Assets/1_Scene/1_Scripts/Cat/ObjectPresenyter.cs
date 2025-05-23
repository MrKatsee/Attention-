using Attention.Process;
using UnityEngine;

namespace Attention
{
    public class ObjectPresenter<T> : ILogicEventHandler
    {
        protected T Object { get; private set; }
    }
}