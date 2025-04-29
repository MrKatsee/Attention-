using Util;

namespace Attention.Process
{
    public class SceneHandler : ILogicEventHandler
    {
        public SceneHandler()
        {

        }

        // TODO: EventType에 따른 함수 캐싱
        // TODO: IEventData를 해당 파라미터에 해당하는 EventData로 캐스팅해서 넣어주기
        // TODO: ex) BoolEventData라면, IEventData -> BoolEventData를 아예 파라미터로 넘겨주는 거
        //[EventHandle(EventType.ChangeScene)]
        //private void ChangeScene()
        //{

        //}

        // TODO: 근데 그냥 EventType 없이 클래스 이름으로 매칭해도 될듯?
        // TODO: 저렇게 한 이유는 파라미터를 캐스팅하기 위해서인데,
        // TODO: 굳이 저렇게 할 필요 없이 데이터 클래스 안에 넣으면 둘 다 동시에 해결 가능,,,?
    }
}