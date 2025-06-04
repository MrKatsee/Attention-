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

        public void UpdateEntity(EntityData data)
        {
            this.transform.position = data.position;
            this._renderer.flipX = data.direction;
        }
    }
}