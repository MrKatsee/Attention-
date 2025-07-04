using Attention.Data;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Attention.View
{
    public class Entity : MonoBehaviour
    {
        //public EntityType type { get; 
        private SpriteRenderer _renderer;

        private Animator _animator;

        public void Init()
        {
            _renderer = this.GetComponent<SpriteRenderer>();
        }

        public void Init(Sprite sprite)
        {
            _renderer = this.GetComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
        }

        public void Init(Sprite sprite, RuntimeAnimatorController controller)
        {
            _renderer = this.GetComponent<SpriteRenderer>();
            _renderer.sprite = sprite;

            _animator = this.AddComponent<Animator>();
            _animator.runtimeAnimatorController = controller;
        }

        public void Init(Sprite sprite, RuntimeAnimatorController controller, int order)
        {
            _renderer = this.GetComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
            _renderer.sortingOrder = order;

            _animator = this.AddComponent<Animator>();
            _animator.runtimeAnimatorController = controller;
        }

        public void UpdateEntity(EntityData data)
        {
            if (data == null) { return; }

            this.transform.position = data.position;
            this._renderer.flipX = data.direction;
            if (_animator != null)
            {
                int anim = _animator.GetInteger("Anim");
                if (anim != data.animator) {
                    _animator.SetInteger("Anim", data.animator);
                    _animator.SetTrigger("Change");
                }
            }
        }
    }
}