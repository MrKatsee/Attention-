using Attention.Data;

namespace Attention
{
    public class BadEnding : EndingData
    {
        public float GetScore(CatData data)
        {
            float score = 0f;

            foreach (Log log in data.logs)
            {
                if (log.listState.Happiness < 50 && log.listState.Fullness < 50)
                {
                    score += 50;
                }
            }

            return score;
        }
    }
}