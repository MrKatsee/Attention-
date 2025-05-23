using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class TimeDataContainer
    {
        private float _processTime;

        public TimeDataContainer()
        {
            _processTime = 0;

            DI.Register(this);
        }

        public void UpdateTime(float deltaTime)
        {
            _processTime += deltaTime;
        }
    }
}
