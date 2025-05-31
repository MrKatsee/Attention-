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
        //[Inject] private CatDataContainer _dataContainter;
        [Inject] private EntityDataContainer _entityDataContainer;

        public CreateCatConrolHandler()
        {
            DI.Register(this);
        }

        public void CreateCat(CreateCatEvent _event)
        {
            Guid id = Guid.NewGuid();
            //_dataContainter.CreateCatData(id);
            _entityDataContainer.CreateEntityData(id);
            _entityLoader.CreateAndBindEntity(id, EntityType.Cat);
        }
    }
}