using Attention;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention
{

    public class DeltaTimeEvent : ILogicEvent
    {
        public float DeltaTime;

        public DeltaTimeEvent(float deltaTime) {
            DeltaTime = deltaTime;
        }

    }

}
