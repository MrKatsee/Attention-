using System.Collections.Generic;
using UnityEngine;

namespace Attention.View
{
    public class UIPrefabContainer : MonoBehaviour
    {
        [SerializeField] private List<UI_Base> _uis;

        private Dictionary<ViewType, UI_Base> _uiDict;

        public void Init()
        {
            _uiDict = new Dictionary<ViewType, UI_Base>();

            foreach (UI_Base ui in _uis)
            {
                _uiDict.Add(ui.Type, ui);
            }
        }

        public UI_Base GetUI(ViewType type)
        {
            if (_uiDict.TryGetValue(type, out UI_Base ui))
            {
                return ui;
            }
            else
            {
                Debug.LogError($"UI of type {type} not found.");
                return null;
            }
        }
    }
}