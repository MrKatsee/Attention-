using System.Collections.Generic;

namespace Attention.Main.EventModule
{
    public interface IEventQueue
    {
        void EnqueueEvent(IEventData eventData);
    }

    public class EventHandler : IEventQueue
    {
        private List<IEventHandler> _eventHandlers;

        private Queue<IEventData> _eventDatas;

        public EventHandler()
        {
            _eventHandlers = new List<IEventHandler>();

            // TODO: 모든 IEventHandler 생성 및 EventType과 함수(파라미터 정보 포함)를 매칭하여 캐싱
        }

        public void Update()
        {
            UpdateBeforeProcess();

            // TODO: 여기서 EventType에 따라서 함수 찾아서, 파라미터까지 캐스팅하여 호출

            UpdateAfterProcess();
        }

        private void UpdateBeforeProcess()
        {

        }

        private void UpdateAfterProcess()
        {

        }

        public void EnqueueEvent(IEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}