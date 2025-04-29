using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Attention.Main
{
    public enum SceneType
    {
        Entry = 0, Scene,
        Loading = 9 // 씬을 로드, 언로드 하는 중간에 활성화된 씬이 하나 이상 필요해서 거쳐 가는 씬
    }

    public interface ISceneLoader
    {
        IEnumerator MoveScene(SceneType from, SceneType to);
    }

    [DIPublisher]
    public class SceneLoader : ISceneLoader
    {
        private int _loadCount = 0;
        public bool IsLoading => _loadCount > 0;

        private SceneType _currentScene;

        public SceneLoader()
        {
            _currentScene = SceneType.Entry;

            DI.Register(this);
        }

        public static string SceneTypeToName(SceneType type)
        {
            switch (type)
            {
                case SceneType.Entry: return "0_Entry";
                case SceneType.Scene: return "1_Scene";
                case SceneType.Loading: return "9_Loading";
                default:
                    throw new InvalidOperationException();
            }
        }

        public IEnumerator MoveScene(SceneType from, SceneType to)
        {
            yield return LoadScene(SceneType.Loading);  // 씬을 로드, 언로드 하는 중간에 활성화된 씬이 하나 이상 필요해서 거쳐 감
            yield return UnloadScene(from);
            yield return LoadScene(to);
            yield return UnloadScene(SceneType.Loading);
        }

        private IEnumerator LoadScene(SceneType type)
        {
            _loadCount++;

            AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(SceneTypeToName(type), LoadSceneMode.Additive);

            while (!asyncLoading.isDone)
            {
                yield return null;
            }

            Scene nextScene = SceneManager.GetSceneByName(SceneTypeToName(type));
            if (nextScene.IsValid())
            {
                while (!nextScene.isLoaded)
                {
                    yield return null;
                }
            }

            _currentScene = type;
        }

        private IEnumerator UnloadScene(SceneType type)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(SceneTypeToName(type));

            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            _loadCount--;
        }

        public IEnumerator UnloadCurrentScene()
        {
            yield return UnloadScene(_currentScene);
        }
    }
}