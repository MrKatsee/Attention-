using Attention.Data;
using System;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class EntityViewPresenter : ViewPresenter<Obj_Entity>
    {
        [Inject(typeof(EntityContainer))] private IEntityContainer _entityContainer;
        //[Inject] private CatDataContainer _dataContainter;
        [Inject] private EntityDataContainer _entityDataContainer;

        public EntityViewPresenter()
        {
            DI.Register(this);
        }

        public void OnEntityViewEvent(EntityUpdateEvent data)
        {
            _entityContainer.UpdateEntity(data.id, _entityDataContainer.GetEntityData(data.id));
        }

        public void OnEntityViewEventByType(EntityUpdateByTypeEvent data)
        {
            foreach (Guid id in _entityContainer.GetIDs())
            {
                Entity entity = _entityContainer.GetEntity(id);
                if (entity != null)
                {
                    entity.UpdateEntity(_entityDataContainer.GetEntityData(id));
                }
            }
        }
    }
}