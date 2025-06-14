using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



namespace Attention.View
{
    public class UI_SettingPanel : UI_Base
    {
        public override ViewType Type => ViewType.SettingPanel;

        private const int MAX_INDEX = 20;

        [SerializeField] private Button _addTaskButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Transform _taskContainer;
        [SerializeField] private Transform _scrollContent;
        [SerializeField] private List<Task> _tasks;

        public void Init(Action addAction, Action quitAction, Action exitAction)
        {
            _addTaskButton.onClick.AddListener(() => { addAction(); });        
            _quitButton.onClick.AddListener(() => {  quitAction(); });
            _exitButton.onClick.AddListener(() => {  exitAction(); });
        }

        public void UpdateTasks(List<string> tasks, Action<int> action)
        {
            List<string> taskList = tasks.Select(s =>
            {
                int index = s.LastIndexOf('\\');
                return (index != -1) ? s.Substring(index + 1) : s;
            }).ToList();
            for (int i=0; i < taskList.Count; ++i)
            {
                if (i >= MAX_INDEX) break;
                Task task = _tasks[i];
                task.transform.SetParent(_scrollContent);
                task.gameObject.SetActive(true);
                task.SetText(taskList[i]);
                task.SetListener(action, i);
            }
        }

        public void ResetTasks()
        {
            foreach (var task in _tasks)
            {
                task.transform.SetParent(_taskContainer);
                task.gameObject.SetActive(false);
            }
        }
    }

}
