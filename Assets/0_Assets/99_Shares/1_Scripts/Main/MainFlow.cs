using Attention.Main.EventModule;
using Attention.Main.InputModule;
using UnityEngine;

namespace Attention.Main
{
    public class MainFlow : MonoBehaviour
    {
        private InputDispatcher _inputDispatcher;

        private void Awake()
        {
            EventBus eventBus = new EventBus();
            _inputDispatcher = new InputDispatcher(eventBus);
        }

        private void Start()
        {

        }

        private void Update()
        {
            _inputDispatcher.Update();

            // TODO: ViewDispatcher (EventHandler랑 비슷하게)
        }
    }
}