using Attention;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention
{

    public class TimeEvent : ILogicEvent
    {
        public float DeltaTime;

        public TimeEvent(float deltaTime) {
            DeltaTime = deltaTime;
        }

    }

}
