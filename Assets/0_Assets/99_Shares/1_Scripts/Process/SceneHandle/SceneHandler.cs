using Attention.Main;
using Attention.Main.EventModule;
using Attention.View;
using System;
using System.Collections;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class SceneHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(SceneLoader))] private ISceneLoader _sceneLoader;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewLoader;

        [Inject] private ViewContainer _viewContainer;
        [Inject] private EntityContainer _entityContainer;

        public SceneHandler()
        {
            DI.Register(this);
        }

        public void ChangeScene(ChangeSceneEvent data)
        {
            CoroutineHelper.Instance.StartRoutine(ChangeSceneRoutine(data.From, data.To));
        }

        private IEnumerator ChangeSceneRoutine(SceneType from, SceneType to)
        {
            yield return _sceneLoader.MoveScene(from, to);

            _viewContainer.InitFactory();   // 순서 주의
            _entityContainer.Init();

            if (from == SceneType.Scene)
            {
                OnExitScene();
            }

            if (to == SceneType.Scene)
            {
                OnEnterScene();
            }

            _eventQueue.EnqueueLogicEvent(new CompleteLoadSceneEvent(to));
        }

        private void OnEnterScene()
        {
            OnNewGame();
            //OnLoadGame(); <- 분기

            //기본 메뉴 패널 
        }

        private void OnNewGame()
        {
            _viewLoader.ActivateView(ViewType.CreateCat);
        }

        private void OnLoadGame()
        {

        }
        
        //분기 상관없이 공통으로 진행되는 Scene 초기 init
        private void OnInitGame()
        {
            //메뉴 패널 생성
            _viewLoader.ActivateView(ViewType.MenuPanel);

        }


        private void OnExitScene()
        {

        }
    }
}