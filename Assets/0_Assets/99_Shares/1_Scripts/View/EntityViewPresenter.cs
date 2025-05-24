using Util;

namespace Attention.View
{
    [DISubscriber]
    public class EntityViewPresenter : ViewPresenter<Obj_Base>
    {
        [Inject] private EntityLoader _entityLoader;

        public EntityViewPresenter()
        {
            DI.Register(this);
        }

        public void CreateEntity(EntityCreateEvent data)
        {
            _entityLoader.CreateEntity(data.type);
        }

        //id 값을 가진 Entity의 View 정보를 갱신
        public void OnEntityViewEvent(EntityUpdateEvent data)
        {
            _entityLoader.UpdateEntity(data.id);
        }
        
        public void OnEntityViewEvent(EntityUpdateByTypeEvent data)
        {
            _entityLoader.UpdateEntityByType(data.type);
        }
    }
}