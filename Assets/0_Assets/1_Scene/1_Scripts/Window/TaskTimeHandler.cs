using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.Process
{

    [DISubscriber]
    public class TaskTimeHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(WindowDataContainer))] private WindowDataContainer _windowDataContainer;
        [Inject(typeof(TaskTimeDataContainer))] private TaskTimeDataContainer _taskTimeDataContainer;
        [Inject(typeof(WindowAPIHandler))] private WindowAPIHandler _windowAPIHandler;
        [Inject(typeof(PlayerDataContainer))] private PlayerDataContainer _playerDataContainer;


        public TaskTimeHandler()
        {
            DI.Register(this);
        }
        private void CheckTaskTime(DeltaTimeEvent data)
        {
            WindowAPIData window = _windowAPIHandler.GetFocusedWindowData();
            List<string> tasks = _windowDataContainer.Tasks;
            if(window == null)
            {
                return;
            }
            if (tasks.Contains(window.ExePath))
            {
                _taskTimeDataContainer.UpdateTime(data.DeltaTime);
                _eventQueue.EnqueueViewEvent(new TaskTimerUpdateViewEvent());

                if (_taskTimeDataContainer.CurrentMoney != _taskTimeDataContainer.PastMoney)
                {
                    _playerDataContainer.AddMoney(_taskTimeDataContainer.CurrentMoney-_taskTimeDataContainer.PastMoney);
                    _taskTimeDataContainer.UpdateMoney();
                    _eventQueue.EnqueueViewEvent(new UpdateMoneyEvent());
                }
            }
        }
        
       

    }

}
