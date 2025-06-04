using Attention.Data;
using Attention.Main.EventModule;
using Attention.Window;
using System.Collections.Generic;
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

        private const float MULTIPLIER = 1.0f;
        public TaskTimeHandler()
        {
            DI.Register(this);
        }
        private void CheckTaskTime(DeltaTimeEvent data)
        {
            WindowAPIData window = _windowAPIHandler.GetFocusedWindowData();
            List<string> tasks = _windowDataContainer.Tasks;
            if(window == null||!_taskTimeDataContainer.IsWorking)
            {
                return;
            }
            if (tasks.Contains(window.ExePath))
            {
                _taskTimeDataContainer.UpdateTime(data.DeltaTime);
                _eventQueue.EnqueueViewEvent(new TaskTimerUpdateViewEvent());
            }
        }
        
        private void UpdateTaskTimerState(TaskTImerWorkingLogicEvent data)
        {
            if(data.IsWorking != _taskTimeDataContainer.IsWorking)
            {
                if (!data.IsWorking)
                {
                    float time = _taskTimeDataContainer.TaskTime;
                    int money = (int)((int)time * MULTIPLIER);
                    _playerDataContainer.AddMoney(money);
                    _taskTimeDataContainer.ResetTime();
                    
                    //TODO: µ∑ ¥ı«œ±‚
                }
                _taskTimeDataContainer.SetWorkingState(data.IsWorking);
                _eventQueue.EnqueueViewEvent(new TaskTimerUpdateViewEvent());
            }
        }

    }

}
