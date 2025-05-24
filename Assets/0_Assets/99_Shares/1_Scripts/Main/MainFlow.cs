using Attention.Data;
using Attention.Main.EventModule;
using Attention.Main.InputModule;
using Attention.View;
using Attention.Window;
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

        private EntityContainer _entityContainer;

        private ObjectLoader _objectLoader;

        private LogicEventRouter _logicEventRouter;
        private ViewEventRouter _viewEventRouter;

        private DataContainer _dataContainer;

        private WindowAPIHandler _windowAPIHandler;

        private void Awake()
        {
            DI.Init();

            _sceneLaoder = new SceneLoader();

            _eventBus = new EventBus();

            _inputDispatcher = new InputDispatcher(_eventBus);

            _logicHandlers = new LogicHandlerContainer();

            _viewPresenters = new ViewPresenterContainer();
            _viewContainer = new ViewContainer(_viewPresenters);

            _entityContainer = new EntityContainer();

            _objectLoader = new ObjectLoader(_viewContainer, _entityContainer);

            _logicEventRouter = new LogicEventRouter(_eventBus, _logicHandlers);
            _viewEventRouter = new ViewEventRouter(_eventBus, _viewPresenters);
            _dataContainer = new DataContainer();
            _windowAPIHandler = new WindowAPIHandler();
        }

        private void Start()
        {
            _logicHandlers.Init();
            _viewPresenters.Init();

            _eventBus.EnqueueLogicEvent(new AttentionWindowLogicEvent());
            _eventBus.EnqueueLogicEvent(new ChangeSceneEvent(SceneType.Entry, SceneType.Scene));
        }

        private void Update()
        {
            if (_sceneLaoder.IsLoading) { return; }

            _eventBus.EnqueueLogicEvent(new DeltaTimeEvent(Time.deltaTime));
            _inputDispatcher.Update();
            _logicEventRouter.Update();
            _objectLoader.Update();
            _viewEventRouter.Update();
        }
    }
}