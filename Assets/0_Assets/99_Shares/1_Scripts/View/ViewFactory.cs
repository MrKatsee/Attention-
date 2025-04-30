using System;
using UnityEngine;

namespace Attention.View
{
    public class ViewFactory
    {
        private UIPrefabContainer _uiPrefabContainer;
        private Transform _canvasTransform;

        public void Init()
        {
            _uiPrefabContainer = GameObject.FindAnyObjectByType<UIPrefabContainer>();
            _uiPrefabContainer.Init();

            _canvasTransform = GameObject.Find("UICanvas").transform;
        }

        public IView CreateView(ViewType type)
        {
            UI_Base uiPrefab = _uiPrefabContainer.GetUI(type);
            if (uiPrefab != null)
            {
                IView uiInstance = GameObject.Instantiate<UI_Base>(uiPrefab, _canvasTransform);
                return uiInstance;
            }
            else
            {   // TODO: UI가 아닌 View
                return null;
            }
        }
    }
}