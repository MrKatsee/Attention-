using Attention.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

[DIPublisher]
public class TestDataContainer : IDataContainer
{
    public int Id;
    public TestDataContainer()
    {
        Id = 0;
        DI.Register(this);
    }
}
