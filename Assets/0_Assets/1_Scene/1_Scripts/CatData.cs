using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention
{
    public class CatData
    {
        public string name;
        public int days;

        public Catstate curState;

        public List<Catstate> stateLogs;
    }

    public class Catstate
    {
        public float satiety;
        public float stress;
        public float affection;
    }
}
