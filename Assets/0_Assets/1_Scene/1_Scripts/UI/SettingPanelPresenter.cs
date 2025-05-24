using Attention.Main.EventModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class SettingPanelPresenter : ViewPresenter<UI_SettingPanel>
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;

        public SettingPanelPresenter()
        {
            DI.Register(this);
        }

        
    }

}
