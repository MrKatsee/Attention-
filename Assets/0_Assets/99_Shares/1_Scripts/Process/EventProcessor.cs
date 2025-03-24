namespace Attention.ProcessModule
{
    public class EventProcessor
    {
        // TODO: 모든 IProcessor를 읽어서, EventType과 함수를 캐싱
        public EventProcessor()
        {

        }

        public void Update()
        {
            UpdateBeforeProcess();

            // TODO: EventQueue에서 처리할 데이터를 분류해서

            UpdateAfterProcess();
        }

        private void UpdateBeforeProcess()
        {

        }

        private void UpdateAfterProcess()
        {

        }
    }
}