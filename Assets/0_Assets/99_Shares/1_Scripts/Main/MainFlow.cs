using Attention.Main.EventModule;
using Attention.Main.InputModule;
using UnityEngine;
using Util;

namespace Attention.Main
{
    public class MainFlow : MonoBehaviour
    {
        private EventBus _eventBus;

        private InputDispatcher _inputDispatcher;

        private LogicHandlerContainer _logicHandlers;
        private ViewPresenterContainer _viewPresenters;

        private LogicEventRouter _logicEventRouter;
        private ViewEventRouter _viewEventRouter;

        private void Awake()
        {
            DI.Init();

            _eventBus = new EventBus();

            _inputDispatcher = new InputDispatcher(_eventBus);

            _logicHandlers = new LogicHandlerContainer();
            _viewPresenters = new ViewPresenterContainer();

            _logicEventRouter = new LogicEventRouter(_eventBus, _logicHandlers);
            _viewEventRouter = new ViewEventRouter(_eventBus, _viewPresenters);
        }

        private void Start()
        {
            _logicHandlers.Init();
            _viewPresenters.Init();
        }

        private void Update()
        {
            _inputDispatcher.Update();
            _logicEventRouter.Update();
            _viewEventRouter.Update();
        }
    }
}