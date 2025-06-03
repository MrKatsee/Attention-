using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace Attention.View
{
    public class UI_SettingPanel : UI_Base
    {
        public override ViewType Type => ViewType.SettingPanel;

        private const int MAX_INDEX = 20;

        [SerializeField] private Button _addTaskButton;
        //[SerializeField] private Button _manageTaskButton;
        [SerializeField] private Button _quitButton;

        [SerializeField] private Transform _taskContainer;
        [SerializeField] private Transform _scrollContent;
        [SerializeField] private List<Task> _tasks;

        public void Init(Action addAction, Action manageAction, Action quitAction)
        {
            _addTaskButton.onClick.AddListener(() => { addAction(); });            
            //_manageTaskButton.onClick.AddListener(() => {  manageAction(); });
            _quitButton.onClick.AddListener(() => {  quitAction(); });
        }

        public void UpdateTasks(List<string> tasks, Action<int> action)
        {
            for(int i=0; i < tasks.Count; ++i)
            {
                if (i >= MAX_INDEX) break;
                Task task = _tasks[i];
                task.transform.SetParent(_scrollContent);
                task.gameObject.SetActive(true);
                task.SetText(tasks[i]);
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

