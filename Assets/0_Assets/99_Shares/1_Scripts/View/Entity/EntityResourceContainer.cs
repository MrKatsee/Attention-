using System.Collections.Generic;
using UnityEngine;

namespace Attention.View
{
    public class EntityResourceContainer : MonoBehaviour
    {
        //[SerializeField] private List<Entity> _entityPrefabList;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<Sprite> _spriteList;
        [SerializeField] private List<RuntimeAnimatorController> _animatorList;

        //private Dictionary<EntityType, Entity> _entities;
        private Dictionary<string, Sprite> _spriteDIct;
        private Dictionary<string, RuntimeAnimatorController> _animatorDIct;

        public EntityResourceContainer() 
        { 
            //_entityPrefabList = new List<Entity>();
            _spriteList = new List<Sprite>();
            _animatorList = new List<RuntimeAnimatorController>();
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
            if(_spriteDIct.ContainsKey(sprteName))
            {
                return _spriteDIct[sprteName];
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