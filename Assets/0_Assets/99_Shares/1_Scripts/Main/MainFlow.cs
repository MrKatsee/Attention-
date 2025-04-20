using Attention.Main.EventModule;
using Attention.Main.InputModule;
using UnityEngine;

namespace Attention.Main
{
    public class MainFlow : MonoBehaviour
    {
        private InputDispatcher _inputDispatcher;
        private EventHandler _eventHandler;

        private void Awake()
        {
            _eventHandler = new EventHandler();
            _inputDispatcher = new InputDispatcher(_eventHandler);
        }

        private void Start()
        {

        }

        private void Update()
        {
            _inputDispatcher.Update();
            _eventHandler.Update();
            // TODO: ViewDispatcher (EventHandler랑 비슷하게)
        }
    }
}