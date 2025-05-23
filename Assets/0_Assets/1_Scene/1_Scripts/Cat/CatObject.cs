using Attention;
using Attention.Process;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatObject : MonoBehaviour, ILogicEventHandler
{
    public void TimeUpdate(DeltaTimeEvent timeEvent)
    {
        this.transform.position += new Vector3(1,0,0) * timeEvent.DeltaTime;
    }
}
