using Attention.Data;
using UnityEngine;

namespace Attention.View
{
    public class Cat_Entity : Entity
    {
        private SpriteRenderer _render;

        public override EntityType type => EntityType.Cat;

        public override void Init()
        {
            _render = this.GetComponent<SpriteRenderer>();
        }

        public override void UpdateEntity(EntityData data)
        {
            base.UpdateEntity(data);
            _render.flipX = data.direction;

            //TODO : 애니메이션 세팅
        }
    }
}