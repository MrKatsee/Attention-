using Attention;
using Attention.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention {
    public class OpenCreateCatUIEvent : ILogicEvent { }

    public class CloseCreateCatUIEvent : IViewEvent { }

    public class CreateCatEvent : ILogicEvent
    {
        public string _catData;

        public CreateCatEvent(string catData)
        {
            _catData = catData;
        }
    }

    public class CompleteLoadSceneEvent : ILogicEvent
    {
        public SceneType _sceneType;

        public CompleteLoadSceneEvent(SceneType sceneType)
        {
            _sceneType = sceneType;
        }
    }

    public class MatchCatImageEvent : IViewEvent
    {
        public string _catData;

        public MatchCatImageEvent(string catData)
        {
            _catData = catData;
        }
    }

    public class OpenStoreEvent : ILogicEvent { }

    public class CloseStoreEvent : IViewEvent { }

    public class OpenSettingEvent : IViewEvent { }

    public class CloseSettingEvent : IViewEvent { }
}