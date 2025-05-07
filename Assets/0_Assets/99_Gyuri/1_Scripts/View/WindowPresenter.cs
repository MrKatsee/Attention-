using Attention.Main.EventModule;
using System.Collections;
using System.Collections.Generic;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class WindowPresenter : ViewPresenter<UI_WinCapture_Button>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        protected override void OnInitializeView()
        {
            View.SetButtonListener(() => { });
        }


    }

}
