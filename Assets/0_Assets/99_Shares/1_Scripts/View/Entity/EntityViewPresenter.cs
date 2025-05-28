using Attention.Data;
using System;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class EntityViewPresenter : ViewPresenter<Obj_Entity>
    {
        [Inject(typeof(EntityContainer))] private IEntityContainer _entityContainer;
        [Inject] private CatDataContainer _dataContainter;

        public EntityViewPresenter()
        {
            DI.Register(this);
        }

        public void OnEntityViewEvent(EntityUpdateByTypeEvent data)
        {
            foreach (Guid id in _entityContainer.GetIDs())
            {
                Entity entity = _entityContainer.GetEntity(id);
                if (entity != null)
                {
                    entity.UpdateEntity(_dataContainter.GetCatData(id));
                }
            }
        }
    }
}