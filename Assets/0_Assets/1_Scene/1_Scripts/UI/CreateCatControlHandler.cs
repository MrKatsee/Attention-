using Attention.Data;
using Attention.View;
using System;
using Util;
using UnityEngine;

namespace Attention.Process
{
    [DISubscriber]
    public class CreateCatConrolHandler : ILogicEventHandler
    {
        [Inject(typeof(EntityLoader))] private IEntityLoader _entityLoader;
        [Inject] private CatDataContainer _dataContainter;
        [Inject] private EntityDataContainer _entityDataContainer;
        [Inject] private GameObjectContainer _objectDataContainer;

        public CreateCatConrolHandler()
        {
            DI.Register(this);
        }

        public void CreateCat(CreateCatEvent _event)
        {
            Guid id = Guid.NewGuid();
            _dataContainter.CreateCatData(id, _event.CatData);
            _dataContainter.SetCurrentCat(id);

            _entityDataContainer.CreateEntityData(id);

            _objectDataContainer.Register(id, new CatObject(id));
            _entityLoader.CreateEntity(new CreateData { id = id, type = EntityType.Cat, sprite = "Idle_0", animator = "Cat" });
        }
    }
}