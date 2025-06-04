using Attention;
using Attention.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention
{
    public class CreateCatEvent : ILogicEvent
    {
        public string CatData;

        public CreateCatEvent(string catData)
        {
            CatData = catData;
        }
    }

    //public class CompleteLoadSceneEvent : ILogicEvent
    //{
    //    public SceneType SceneType;

    //    public CompleteLoadSceneEvent(SceneType sceneType)
    //    {
    //        SceneType = sceneType;
    //    }
    //}

    public class MatchCatImageEvent : IViewEvent
    {
        public string CatData;

        public MatchCatImageEvent(string catData)
        {
            CatData = catData;
        }
    }

    public class TimeViewEvent : IViewEvent
    {
        public float DeltaTime;

        public TimeViewEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }

    public class OpenStoreEvent : ILogicEvent { }
}