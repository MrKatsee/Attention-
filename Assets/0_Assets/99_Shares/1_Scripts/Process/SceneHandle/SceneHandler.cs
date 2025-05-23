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
        [Inject(typeof(EventBus))] 
        private IEventQueue _eventQueue;
        
        [Inject(typeof(SceneLoader))] 
        private ISceneLoader _sceneLoader;
        
        [Inject] private ViewContainer _viewContainer;

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
        }

        private void OnNewGame()
        {
            _viewContainer.ActivateView(ViewType.CreateCat);
        }

        private void OnLoadGame()
        {

        }

        private void OnExitScene()
        {

        }
    }
}