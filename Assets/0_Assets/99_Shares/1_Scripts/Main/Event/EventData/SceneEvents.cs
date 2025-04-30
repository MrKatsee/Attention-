using Attention.Main;

namespace Attention
{
    public class ChangeSceneEvent : ILogicEvent
    {
        public SceneType From { get; }
        public SceneType To { get; }

        public ChangeSceneEvent(SceneType from, SceneType to)
        {
            From = from;
            To = to;
        }
    }
}