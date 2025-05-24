using UnityEngine;

namespace Attention.View
{
    public class ViewFactory
    {
        private UIPrefabContainer _uiPrefabContainer;
        //private ObjPrefabContainer _objPrefabContainer;
        private Transform _canvasTransform;

        public void Init()
        {
            _uiPrefabContainer = GameObject.FindAnyObjectByType<UIPrefabContainer>();
            _uiPrefabContainer.Init();

            //_objPrefabContainer = GameObject.FindAnyObjectByType<ObjPrefabContainer>();
            //_objPrefabContainer.Init();

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

            //Obj_Base objPrefab = _objPrefabContainer.GetObj(type);
            //if(objPrefab != null)
            //{
            //    IView objInstance = GameObject.Instantiate<Obj_Base>(objPrefab);
            //    return objPrefab;
            //}

            return null;
        }
    }
}