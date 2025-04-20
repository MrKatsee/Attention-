using Attention.Main.EventModule;
using Attention.Main.InputModule;
using UnityEngine;

namespace Attention.Main
{
    public class MainFlow : MonoBehaviour
    {
        private EventBus _eventBus;

        private InputDispatcher _inputDispatcher;
        private LogicEventRouter _logicEventRouter;
        //private ViewEventHandler _viewEventHandler;

        private void Awake()
        {
            _eventBus = new EventBus();

            _inputDispatcher = new InputDispatcher(_eventBus);
            _logicEventRouter = new LogicEventRouter(_eventBus);
        }

        private void Start()
        {
            _logicEventRouter.RegisterEvents();
        }

        private void Update()
        {
            _inputDispatcher.Update();
            _logicEventRouter.Update();
        }
    }
}