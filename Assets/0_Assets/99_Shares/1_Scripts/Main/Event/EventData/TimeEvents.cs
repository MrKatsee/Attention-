namespace Attention
{

    public class DeltaTimeEvent : ILogicEvent
    {
        public float DeltaTime;

        public DeltaTimeEvent(float deltaTime) {
            DeltaTime = deltaTime;
        }

    }

}
