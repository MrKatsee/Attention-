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

        [SerializeField] private Button _addTaskButton;
        [SerializeField] private Button _manageTaskButton;
        [SerializeField] private Button _quitButton;

        public void Init(Action addAction, Action manageAction, Action quitAction)
        {
            _addTaskButton.onClick.AddListener(() => { addAction(); });            
            _manageTaskButton.onClick.AddListener(() => {  manageAction(); });
            _quitButton.onClick.AddListener(() => {  quitAction(); });
        }        
    }

}

