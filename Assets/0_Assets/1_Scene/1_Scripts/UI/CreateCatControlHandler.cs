using Attention.Main.EventModule;
using Attention.Process;
using Attention.View;
using Util;

namespace Attention
{
    [DISubscriber]
    public class CreateCatConrolHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
        [Inject(typeof(EntityLoader))] private IEntityLoader _entityLoader;

        public CreateCatConrolHandler()
        {
            DI.Register(this);
        }

        public void CreateCat(CreateCatEvent _event)
        {
            _entityLoader.CreateEntity(EntityType.Cat);
            _eventQueue.EnqueueViewEvent(new MatchCatImageEvent(_event.CatData));
        }
    }
}