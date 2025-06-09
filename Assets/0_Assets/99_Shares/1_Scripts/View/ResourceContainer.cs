using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Attention.View
{
    public class ResourceContainer : MonoBehaviour
    {
        //[SerializeField] private List<Entity> _entityPrefabList;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<Sprite> _spriteList;
        [SerializeField] private List<RuntimeAnimatorController> _animatorList;
        [SerializeField] private List<string> _endingDescList;

        //private Dictionary<EntityType, Entity> _entities;
        private Dictionary<string, Sprite> _spriteDIct;
        private Dictionary<string, RuntimeAnimatorController> _animatorDIct;
        private Dictionary<string, string> _endingTitleDict;
        private Dictionary<string, string> _endingDescDict;

        public ResourceContainer() 
        { 
            //_entityPrefabList = new List<Entity>();
            _spriteList = new List<Sprite>();
            _animatorList = new List<RuntimeAnimatorController>();
            _endingDescList = new List<string>();
        }

        public void Init()
        {
            //_entities = new Dictionary<EntityType, Entity>();

            //foreach (Entity entity in _entityPrefabList)
            //{
            //    _entities.Add(entity.type, entity);
            //}

            _spriteDIct = new Dictionary<string, Sprite>();
            foreach (Sprite sprite in _spriteList)
            {
                _spriteDIct.Add(sprite.name, sprite);
            }

            _animatorDIct = new Dictionary<string, RuntimeAnimatorController>();
            foreach (RuntimeAnimatorController animator in _animatorList)
            {
                _animatorDIct.Add(animator.name, animator);
            }

            _endingTitleDict = new Dictionary<string, string>();
            _endingDescDict = new Dictionary<string, string>();

            foreach (string key in _endingDescList)
            {
                string[] datas = key.Split('_');
                if(datas.Length != 3)
                {
                    continue;
                }

                _endingTitleDict[datas[0]] = datas[1];
                _endingDescDict[datas[0]] = datas[2];
            }
        }
        public Entity GetEntity()
        {
            return prefab.GetComponent<Entity>();
        }

        public Entity GetEntity(EntityType type)
        {
            //if(_entities.ContainsKey(type))
            //{
            //    return _entities[type];
            //}

            //return null;
            return prefab.GetComponent<Entity>();
        }

        public Sprite GetSprite(string sprteName)
        {
            string debug = "";
            foreach (string sprite in _spriteDIct.Keys)
            {
                debug += sprite + " : " + (sprite == sprteName) +"\n";
            }
            Debug.Log(debug);

            if(_spriteDIct.ContainsKey(sprteName))
            {
                return _spriteDIct[sprteName];
            }
            return null;
        }

        public string GetEndingTitle(string ending)
        {
            if(_endingTitleDict.ContainsKey(ending))
            {
                return _endingTitleDict[ending];
            }
            return null;
        }

        public string GetEndingDesc(string ending)
        {
            if ( _endingDescDict.ContainsKey(ending))
            {
                return _endingDescDict[ending];
            }
            return null;
        }

        public RuntimeAnimatorController GetAnimator(string animatorName)
        {
            if (_animatorDIct.ContainsKey(animatorName))
            {
                return _animatorDIct[animatorName];
            }
            else
            {
                UnityEngine.Debug.Log(animatorName);
                string debuging = "";
                foreach (var name in _animatorDIct.Keys)
                {
                    debuging += name + "\n";
                }
                UnityEngine.Debug.Log(debuging);
            }
            return null;
        }
    }
}