using Attention.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

[DIPublisher]
public class TestDataContainer : IDataContainer
{
    public int id;
    public TestDataContainer()
    {
        id = 3;
        DI.Register(this);
    }
}
