using Attention.Data;
using System.Collections.Generic;

namespace Attention
{
    public class QueneEnding : EndingData
    {
        public float GetScore(CatData data)
        {
            float score = -1;
            if (data.Happiness > 90 && data.Fullness > 90 && data.Cleanliness > 90 && data.Bond > 90)
            {
                score = 1000;
            }

            return score;
        }
    }
}