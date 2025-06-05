using Attention.Data;
using System.Collections.Generic;
using Util;

namespace Attention
{
    [DIPublisher]
    public class EndingManager
    {
        private Dictionary<string, EndingData> endingDict;

        public EndingManager()
        {
            endingDict = new Dictionary<string, EndingData>();
            DI.Register(this);
        }

        public void resgistEnding(string name, EndingData data)
        {
            endingDict[name] = data;
        }

        public string GetEnding(CatData data)
        {
            float maxScore = 0;
            string ending = "";
            foreach (string key in endingDict.Keys)
            {
                float score = endingDict[key].GetScore(data);
                if (score > maxScore)
                {
                    maxScore = score;
                    ending = key;
                }
            }

            return ending;
        }
    }

    public interface EndingData
    {
        public float GetScore(CatData data);
    }
}