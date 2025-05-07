using Attention.Main.EventModule;
using System.Numerics;
using Util;

namespace Attention.View
{
    public class TestPresenter : ViewPresenter<UI_Test>
    {
        
        public void Test(TestUIEvent data)
        {
            View.SetText(data.ScreenPosition.ToString());
        }
    }

    public class TestButtonPresenter: ViewPresenter<UI_Button_test>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;

        protected override void OnInitializeView()
        {
            SetOnClick();
        }
        public void SetOnClick()
        {
            View.SetButtonListener(
                () => {
                    _eventQueue.EnqueueLogicEvent(new SelectClickEvent(UnityEngine.Vector3.zero));
                    //_eventQueue.EnqueueViewEvent(new TestUIEvent(UnityEngine.Vector3.zero));
                });
        }

        
    }
}