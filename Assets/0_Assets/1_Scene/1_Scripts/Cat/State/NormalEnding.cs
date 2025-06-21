using Attention.Data;
using System.Collections.Generic;

namespace Attention
{
    public class NormalEnding : EndingData
    {
        public float GetScore(CatData data)
        {
            float score = -1;
            if (data.Happiness > 20 && data.Fullness > 20 && data.Cleanliness > 20 && data.Bond > 20)
            {
                score = 500;
            }

            return score;
        }
    }
}