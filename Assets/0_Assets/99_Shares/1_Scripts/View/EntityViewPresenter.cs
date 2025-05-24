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

        //id ���� ���� Entity�� View ������ ����
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