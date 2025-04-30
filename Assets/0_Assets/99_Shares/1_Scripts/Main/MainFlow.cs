using Attention.Main.EventModule;
using Attention.Main.InputModule;
using Attention.View;
using UnityEngine;
using Util;

namespace Attention.Main
{
    public class MainFlow : MonoBehaviour
    {
        private SceneLoader _sceneLaoder;

        private EventBus _eventBus;

        private InputDispatcher _inputDispatcher;

        private LogicHandlerContainer _logicHandlers;

        private ViewPresenterContainer _viewPresenters;
        private ViewContainer _viewContainer;

        private LogicEventRouter _logicEventRouter;
        private ViewEventRouter _viewEventRouter;

        private void Awake()
        {
            DI.Init();

            _sceneLaoder = new SceneLoader();

            _eventBus = new EventBus();

            _inputDispatcher = new InputDispatcher(_eventBus);

            _logicHandlers = new LogicHandlerContainer();

            _viewPresenters = new ViewPresenterContainer();
            _viewContainer = new ViewContainer(_viewPresenters);

            _logicEventRouter = new LogicEventRouter(_eventBus, _logicHandlers);
            _viewEventRouter = new ViewEventRouter(_eventBus, _viewPresenters);
        }

        private void Start()
        {
            _logicHandlers.Init();
            _viewPresenters.Init();

            _eventBus.EnqueueLogicEvent(new ChangeSceneEvent(SceneType.Entry, SceneType.Scene));
        }

        private void Update()
        {
            if (_sceneLaoder.IsLoading) { return; }

            _inputDispatcher.Update();
            _logicEventRouter.Update();
            _viewEventRouter.Update();
        }
    }
}