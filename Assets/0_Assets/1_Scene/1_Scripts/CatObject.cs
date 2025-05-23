using Attention;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatObject : MonoBehaviour
{
    public void TimeUpdate(DeltaTimeEvent timeEvent)
    {
        this.transform.position += new Vector3(1,0,0) * timeEvent.DeltaTime;
    }
}
