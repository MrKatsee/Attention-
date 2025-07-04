using Attention.Data;
using Attention.View;
using System;
using Util;

namespace Attention.Process
{
    [DISubscriber]
    public class CreateCatConrolHandler : ILogicEventHandler
    {
        [Inject(typeof(EntityLoader))] private IEntityLoader _entityLoader;
        [Inject] private CatDataContainer _dataContainter;
        [Inject] private EntityDataContainer _entityDataContainer;
        [Inject] private GameObjectContainer _objectDataContainer;
        [Inject] private BehaviorContainer _behaviorContainer;

        public CreateCatConrolHandler()
        {
            DI.Register(this);
        }

        public void CreateCat(CreateCatEvent _event)
        {
            Guid id = Guid.NewGuid();
            _dataContainter.CreateCatData(id, _event.CatData);
            _dataContainter.SetCurrentCat(id);

            _entityDataContainer.CreateEntityData(id, true);

            //_objectDataContainer.Register(id, new CatObject(id));
            _behaviorContainer.Register(id, new IdleBehavior(3));
            _entityLoader.CreateEntity(new CreateData { id = id, type = EntityType.Cat, sprite = "Idle_0", animator = "Cat", order = 10 });
        }

        public void RemoveCat(EntityRemoveEvent _event)
        {
            _dataContainter.SetCurrentCat(Guid.Empty);

            _entityDataContainer.RemoveEntityData(_event.id);

            _objectDataContainer.Remove(_event.id);

            _entityLoader.DeactivateEntity(_event.id);
        }
    }
}