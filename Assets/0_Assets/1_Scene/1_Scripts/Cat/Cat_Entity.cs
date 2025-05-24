using Attention.Data;
using UnityEngine;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class Cat_Entity : Entity
    {
        [Inject] private CatDataContainer _entityDataContainer;

        private SpriteRenderer _render;

        public override EntityType type => EntityType.Cat;

        public override void Init()
        {
            base.Init();
            DI.Register(this);
            _entityDataContainer.CreateCataData(id, new CatEntityData());
            _render = this.GetComponent<SpriteRenderer>();
        }

        public override void UpdateEntity()
        {
            EntityData data = _entityDataContainer.GetCatData(id);
            if (data != null && data is CatEntityData catData)
            {
                UpdateEntity(catData);
            }
        }

        public void UpdateEntity(CatEntityData data)
        {
            this.transform.position = data.position;
            _render.flipX = data.direction;

            //TODO : 애니메이션 세팅
        }
    }
}