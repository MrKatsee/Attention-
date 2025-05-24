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

        public CreateCatConrolHandler()
        {
            DI.Register(this);
        }

        public void CreateCat(CreateCatEvent _event)
        {
            Guid id = Guid.NewGuid();
            _dataContainter.CreateCatData(id);
            _entityLoader.CreateAndBindEntity(id, EntityType.Cat);
        }
    }
}