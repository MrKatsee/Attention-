
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class TimeDataContainer : IDataContainer
    {
        public float ProcessTime;

        public TimeDataContainer()
        {
            ProcessTime = 0;
            Debug.Log("wowow" + ProcessTime);
            DI.Register(this);
        }
    }
}
