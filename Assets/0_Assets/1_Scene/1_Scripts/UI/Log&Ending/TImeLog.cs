using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attention
{
    public class TimeLog : MonoBehaviour
    {
        [SerializeField] private List<Slider> timeGraph;

        public TimeLog()
        {
            timeGraph = new List<Slider>();
        }

        public void Set(int maxTime, List<float> timeRecods)
        {
            if(maxTime == 0) { return; }

            for (int i = 0; i < timeGraph.Count; i++)
            {
                if(i < timeRecods.Count)
                {
                    timeGraph[i].gameObject.SetActive(true);
                    timeGraph[i].value = timeRecods[i] / maxTime;
                }
                else
                {
                    timeGraph[i].gameObject.SetActive(false);
                }
            }
        }
    }
}