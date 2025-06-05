using Attention.Data;
using System.Collections.Generic;

namespace Attention
{
    public class QueneEnding : EndingData
    {
        public float GetScore(CatData data)
        {
            float score = 0;
            foreach (State state in data.stateLogs)
            {
                if (state.Happiness > 70 && state.Fullness > 50)
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