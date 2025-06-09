using Attention.Data;
using System.Collections.Generic;

namespace Attention
{
    public class QueneEnding : EndingData
    {
        public float GetScore(CatData data)
        {
            float score = -1;
            if (data.Happiness > 40 && data.Fullness > 40 && data.Cleanliness > 40 && data.Bond > 40)
            {
                score += 100;
            }

            foreach (Log log in data.logs)
            {
                if (log.listState.Happiness > 70 && log.listState.Fullness > 50)
                {
                    score += 20;
                }
            }

            if(data.Bond > 80)
            {
                score += 70;
            }
            return score;
        }
    }
}